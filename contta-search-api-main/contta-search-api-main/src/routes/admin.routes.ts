import { Router } from 'express';
import fetch from 'node-fetch';

const adminRouter = Router();

// POST /api/admin/invite { email, firstName?, lastName?, roles?: string[] }
adminRouter.post('/admin/invite', async (req, res) => {
  try {
  const currentUser = (req as any).user as any;
  const realmRoles: string[] = (currentUser && currentUser.realm_access && currentUser.realm_access.roles) || [];
  if (realmRoles.indexOf('admin') === -1) {
      return res.status(403).json({ error: 'forbidden' });
    }
  const { email, firstName = '', lastName = '', roles: assignRoles = [] } = req.body || {};
    if (!email) return res.status(400).json({ error: 'email is required' });

    const baseUrl = process.env.KEYCLOAK_URL || 'http://keycloak:8080';
    const realm = process.env.KEYCLOAK_REALM || 'contta';
    const adminUser = process.env.KEYCLOAK_ADMIN_USER || 'admin';
    const adminPass = process.env.KEYCLOAK_ADMIN_PASSWORD || 'admin';

    // 1) Get admin access token
    const tokenResp = await fetch(`${baseUrl}/realms/master/protocol/openid-connect/token`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
      body: new URLSearchParams({
        grant_type: 'password',
        client_id: 'admin-cli',
        username: adminUser,
        password: adminPass,
      }),
    });
    if (!tokenResp.ok) {
      const text = await tokenResp.text();
      return res.status(502).json({ error: 'keycloak_token_error', details: text });
    }
    const tokenJson = (await tokenResp.json()) as any;
    const accessToken = tokenJson.access_token as string;

    // 2) Create or update user
    const userPayload = {
      username: email,
      email,
      enabled: true,
      emailVerified: false,
      firstName,
      lastName,
    };
    const createResp = await fetch(`${baseUrl}/admin/realms/${realm}/users`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${accessToken}`,
      },
      body: JSON.stringify(userPayload),
    });

    if (createResp.status !== 201 && createResp.status !== 409) {
      const text = await createResp.text();
      return res.status(502).json({ error: 'keycloak_create_user_error', details: text });
    }

    // 3) Find user id (on conflict or after create)
    const findResp = await fetch(
      `${baseUrl}/admin/realms/${realm}/users?email=${encodeURIComponent(email)}`,
      { headers: { Authorization: `Bearer ${accessToken}` } },
    );
  const users = (await findResp.json()) as any[];
  const foundUser = users.find((u) => (u.email || '').toLowerCase() === email.toLowerCase());
  if (!foundUser) return res.status(500).json({ error: 'user_not_found_after_create' });

    // 4) Assign realm roles if provided
  if (assignRoles && assignRoles.length) {
      // fetch realm roles
      const rolesResp = await fetch(`${baseUrl}/admin/realms/${realm}/roles`, {
        headers: { Authorization: `Bearer ${accessToken}` },
      });
      const available = (await rolesResp.json()) as any[];
      const toAssign = available.filter((r) => assignRoles.indexOf(r.name) !== -1);
      if (toAssign.length) {
        await fetch(`${baseUrl}/admin/realms/${realm}/users/${foundUser.id}/role-mappings/realm`, {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
            Authorization: `Bearer ${accessToken}`,
          },
          body: JSON.stringify(toAssign.map((r) => ({ id: r.id, name: r.name }))),
        });
      }
    }

    // 5) Send verify email (acts like an invite link)
    const verifyResp = await fetch(
  `${baseUrl}/admin/realms/${realm}/users/${foundUser.id}/send-verify-email`,
      {
        method: 'PUT',
        headers: { Authorization: `Bearer ${accessToken}` },
      },
    );
    if (!verifyResp.ok) {
      const text = await verifyResp.text();
      return res.status(502).json({ error: 'keycloak_send_email_error', details: text });
    }

  return res.status(200).json({ ok: true, userId: foundUser.id });
  } catch (e: any) {
    return res.status(500).json({ error: 'unexpected', message: e?.message });
  }
});

export { adminRouter };
