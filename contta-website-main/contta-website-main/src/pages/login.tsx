import React from 'react';
import { createCodeChallenge, createCodeVerifier } from '../../utils/pkce';

const issuer = process.env.NEXT_PUBLIC_OIDC_ISSUER || 'http://localhost:8080/realms/contta';
const clientId = process.env.NEXT_PUBLIC_OIDC_CLIENT_ID || 'contta-portal';

export default function Login() {
  const handleLogin = async () => {
    if (typeof window === 'undefined') return;
    const redirectUri = `${window.location.origin}/auth/callback`;
    const codeVerifier = createCodeVerifier();
    const codeChallenge = await createCodeChallenge(codeVerifier);
    sessionStorage.setItem('pkce_verifier', codeVerifier);

    const u = new URL(`${issuer}/protocol/openid-connect/auth`);
    u.searchParams.set('client_id', clientId);
    u.searchParams.set('redirect_uri', redirectUri);
    u.searchParams.set('response_type', 'code');
    u.searchParams.set('scope', 'openid profile email');
    u.searchParams.set('code_challenge_method', 'S256');
    u.searchParams.set('code_challenge', codeChallenge);
    u.searchParams.set('prompt', 'login');
    window.location.href = u.toString();
  };

  return (
    <div style={{ display: 'flex', minHeight: '70vh', alignItems: 'center', justifyContent: 'center' }}>
      <div style={{ maxWidth: 420, width: '100%', padding: 24, border: '1px solid #eee', borderRadius: 12 }}>
        <h1 style={{ marginBottom: 8 }}>Entrar</h1>
        <p style={{ color: '#666', marginBottom: 24 }}>Acesse com sua conta para continuar.</p>
        <button onClick={handleLogin} style={{ width: '100%', padding: 12, borderRadius: 8, background: '#111827', color: '#fff', border: 0 }}>
          Continuar com Contta (Keycloak)
        </button>
      </div>
    </div>
  );
}
