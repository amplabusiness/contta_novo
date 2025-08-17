import React from 'react';
import { AuthProvider } from 'react-oidc-context';

const oidcConfig = {
  authority: process.env.REACT_APP_OIDC_AUTHORITY || 'http://localhost:8080/realms/contta',
  client_id: process.env.REACT_APP_OIDC_CLIENT_ID || 'contta-portal',
  redirect_uri: process.env.REACT_APP_OIDC_REDIRECT_URI || 'http://localhost:3000',
  automaticSilentRenew: true,
  loadUserInfo: true,
};

const OidcAuthProvider = ({ children }) => {
  const enabled = process.env.REACT_APP_AUTH_MODE === 'oidc';
  if (!enabled) return children;
  return <AuthProvider {...oidcConfig}>{children}</AuthProvider>;
};

export default OidcAuthProvider;
