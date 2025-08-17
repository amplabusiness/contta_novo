import { NextFunction, Request, Response } from 'express';
import { createRemoteJWKSet, jwtVerify } from 'jose';

const OIDC_ISSUER = process.env.OIDC_ISSUER || 'http://localhost:8080/realms/contta';
const OIDC_AUDIENCE = process.env.OIDC_AUDIENCE || 'contta-portal';

const jwks = createRemoteJWKSet(new URL(`${OIDC_ISSUER}/protocol/openid-connect/certs`));

export async function authMiddleware(req: Request, res: Response, next: NextFunction) {
  try {
    if (req.path === '/health') return next();

    const auth = req.headers.authorization || '';
    const [, token] = auth.split(' ');
    if (!token) return res.status(401).json({ error: 'Unauthorized' });

    const { payload } = await jwtVerify(token, jwks, {
      issuer: OIDC_ISSUER,
      audience: OIDC_AUDIENCE,
    });

    (req as any).user = payload;
    return next();
  } catch (err) {
    return res.status(401).json({ error: 'Invalid token' });
  }
}
