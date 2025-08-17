export function createCodeVerifier(len = 48) {
  const chars = 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._~';
  let s = '';
  const array = new Uint8Array(len);
  if (typeof window !== 'undefined' && window.crypto) {
    window.crypto.getRandomValues(array);
  } else {
    for (let i = 0; i < len; i++) array[i] = Math.floor(Math.random() * 256);
  }
  for (let i = 0; i < len; i++) s += chars[array[i] % chars.length];
  return s;
}

export async function createCodeChallenge(verifier: string) {
  const data = new TextEncoder().encode(verifier);
  const digest = await (window.crypto || ({} as any).subtle).subtle.digest('SHA-256', data);
  const base64 = btoa(String.fromCharCode(...new Uint8Array(digest as ArrayBuffer)));
  return base64.replace(/\+/g, '-').replace(/\//g, '_').replace(/=+$/, '');
}
