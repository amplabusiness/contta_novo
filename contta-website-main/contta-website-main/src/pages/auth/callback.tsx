import React, { useEffect, useState } from 'react';

const issuer = process.env.NEXT_PUBLIC_OIDC_ISSUER || 'http://localhost:8080/realms/contta';
const clientId = process.env.NEXT_PUBLIC_OIDC_CLIENT_ID || 'contta-portal';

export default function Callback() {
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const run = async () => {
      try {
        const params = new URLSearchParams(window.location.search);
        const code = params.get('code');
        if (!code) throw new Error('code ausente');
        const verifier = sessionStorage.getItem('pkce_verifier') || '';
        if (!verifier) throw new Error('verifier ausente');

        const tokenUrl = `${issuer}/protocol/openid-connect/token`;
        const body = new URLSearchParams({
          grant_type: 'authorization_code',
          code,
          client_id: clientId,
          redirect_uri: `${window.location.origin}/auth/callback`,
          code_verifier: verifier,
        });
        const resp = await fetch(tokenUrl, {
          method: 'POST',
          headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
          body,
        });
        if (!resp.ok) {
          const t = await resp.text();
          throw new Error(`token error: ${t}`);
        }
        const tokens = await resp.json();
        sessionStorage.setItem('id_token', tokens.id_token);
        sessionStorage.setItem('access_token', tokens.access_token);
        sessionStorage.removeItem('pkce_verifier');
        window.location.replace('/');
      } catch (e: any) {
        setError(e?.message || 'erro');
      }
    };
    run();
  }, []);

  return <div style={{ padding: 24 }}>{error ? `Erro: ${error}` : 'Processando login...'}</div>;
}
