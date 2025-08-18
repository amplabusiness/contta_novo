# Login e Tokens — Atual vs. Keycloak OIDC + PKCE

Este documento descreve o fluxo de autenticação atual (alto nível) e o alvo com Keycloak usando Authorization Code + PKCE, indicando tokens, armazenamento, emissão/refresh, validação no back-end e variáveis de ambiente.

## Visão rápida
- Protocolo: OpenID Connect (OIDC) Authorization Code com PKCE (S256).
- Front-end (SPA): cliente público no Keycloak; usa code + code_verifier; armazena o access token em memória; refresh via cookie httpOnly no domínio do back-end.
- Back-end (APIs .NET): apenas valida os JWTs emitidos pelo Keycloak (RS256 via JWKS). Não guarda segredos do IdP.

## Fluxo alvo (Authorization Code + PKCE)

1) SPA inicia login
- Gera `code_verifier` (alta entropia) e `code_challenge = SHA256(base64url(code_verifier))`.
- Redireciona para `KEYCLOAK_ISSUER/oauth2/authorize` com:
  - `client_id=SPA_CLIENT_ID`
  - `redirect_uri=https://app.e-contta.com.br/callback`
  - `response_type=code`
  - `scope=openid profile email`
  - `code_challenge=<...>` e `code_challenge_method=S256`

2) Keycloak autentica o usuário e redireciona de volta para o `redirect_uri` com `code`.

3) Exchange do code no back-end (recomendado)
- A SPA chama o endpoint do back-end `/auth/callback?code=...` enviando também o `code_verifier` (via body).
- O back-end troca `code` + `code_verifier` por `access_token`, `id_token` e `refresh_token` no `KEYCLOAK_TOKEN_ENDPOINT`.
- O back-end retorna para a SPA:
  - Access token apenas no corpo (ou cabeçalho) da resposta; mantido em memória pela SPA.
  - Refresh token somente em cookie httpOnly, Secure, SameSite=Lax no domínio da API (evitando XSS).

4) Consumo de APIs
- SPA envia o `access_token` no header `Authorization: Bearer <token>`.
- API valida assinatura (JWKS), `iss`, `aud`, `exp`, `nbf`, `alg`, `kid`.

5) Refresh
- Quando o access token expira, a SPA chama `/auth/refresh`; o back-end lê o refresh token do cookie httpOnly, solicita novo par de tokens ao Keycloak e retorna novo access token (e rotaciona o refresh token no cookie).

6) Logout
- SPA chama `/auth/logout` no back-end que:
  - Revoga o refresh token no Keycloak
  - Limpa cookies httpOnly
  - Opcionalmente dispara RP-Initiated Logout em `end_session_endpoint` e redireciona para a home.

## Diagrama (sequência)

```mermaid
sequenceDiagram
  participant U as User
  participant SPA as Front-end (SPA)
  participant API as Back-end API
  participant KC as Keycloak

  U->>SPA: Acessa /login
  SPA->>SPA: Gera code_verifier + code_challenge
  SPA->>KC: GET /authorize (code, PKCE)
  KC-->>SPA: 302 redirect (code)
  SPA->>API: POST /auth/callback (code, code_verifier)
  API->>KC: POST /token (code, code_verifier)
  KC-->>API: access_token, id_token, refresh_token
  API->>SPA: access_token (body); set-cookie httpOnly refresh_token
  SPA->>API: GET /api/* (Authorization: Bearer access_token)
  API->>KC: (JWKS cache) valida assinatura/claims
  API-->>SPA: 200 OK
  SPA->>API: POST /auth/refresh (cookie refresh)
  API->>KC: POST /token (refresh_token)
  KC-->>API: novos tokens
  API-->>SPA: novo access_token; set-cookie refresh rotacionado
```

## Tokens e claims
- id_token: identidade do usuário (OIDC); uso primário no front para exibir nome/email.
- access_token: autorização para APIs (escopos/roles); curta duração (5–15 min).
- refresh_token: obtém novos access tokens; mantido apenas em cookie httpOnly.
- Roles/claims:
  - `realm_access.roles` e `resource_access[client].roles` usados para policies no back-end.

## Validação no back-end (.NET)
- JwtBearer com Authority = `KEYCLOAK_ISSUER` (/.well-known/openid-configuration)
- Validar:
  - Assinatura (RS256/RS512) via JWKS
  - `iss` = issuer do realm
  - `aud` = client_id da API (ou configuração de audience)
  - `exp`, `nbf` (clock skew baixo, ex.: 2 min)
  - `alg` e `kid` válidos
- Mapear roles para `ClaimsIdentity` e políticas `[Authorize(Policy = "role:x")]`.

## Armazenamento seguro no front
- Access token em memória (não em localStorage/sessionStorage).
- Refresh token somente em cookie httpOnly; Secure; SameSite=Lax; domain da API.
- CSRF: rotas de refresh/logout devem exigir header custom (ex.: `X-Requested-With`) e checar origem.

## Configuração Keycloak
- Realm: `contta`
- Client (SPA):
  - `clientId = contta-spa`
  - Public client = true
  - Standard Flow = on (Authorization Code)
  - PKCE = S256 required
  - Valid Redirect URIs: `https://app.e-contta.com.br/*`
  - Web origins: `https://app.e-contta.com.br`
- (Opcional) Client (API confidencial): apenas se API precisar obter tokens por si (client credentials). Para simples validação, não é necessário.

## Variáveis de ambiente
- Back-end
  - `KEYCLOAK_ISSUER=https://sso.e-contta.com.br/realms/contta`
  - `KEYCLOAK_JWKS=https://sso.e-contta.com.br/realms/contta/protocol/openid-connect/certs`
  - `OIDC_AUDIENCE=contta-api` (se exigir audience)
  - `OIDC_REQUIRED_SCOPES=openid profile email`
  - `AUTH_COOKIE_NAME=contta_rt`
  - `AUTH_COOKIE_DOMAIN=.e-contta.com.br`
  - `AUTH_COOKIE_SECURE=true`
- Front-end
  - `VITE_OIDC_CLIENT_ID=contta-spa`
  - `VITE_OIDC_ISSUER=https://sso.e-contta.com.br/realms/contta`
  - `VITE_OIDC_REDIRECT_URI=https://app.e-contta.com.br/callback`

## Exemplo (ASP.NET Core – JwtBearer)

```csharp
services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Authority = Environment.GetEnvironmentVariable("KEYCLOAK_ISSUER");
    options.MetadataAddress = $"{options.Authority}/.well-known/openid-configuration";
    options.RequireHttpsMetadata = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = Environment.GetEnvironmentVariable("KEYCLOAK_ISSUER"),
        ValidateAudience = true,
        ValidAudience = Environment.GetEnvironmentVariable("OIDC_AUDIENCE"),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(2),
        ValidateIssuerSigningKey = true,
        NameClaimType = "preferred_username",
        RoleClaimType = "roles" // map via claims transformation se necessário
    };
});
```

## Checklist de segurança
- [ ] PKCE S256 obrigatório
- [ ] Access token curto (<= 15 min); refresh rotacionado
- [ ] Refresh apenas em cookie httpOnly, Secure, SameSite=Lax
- [ ] Validar iss/aud/alg/exp/nbf; JWKS com cache e rotação de keys
- [ ] CORS restrito às origens Vercel/produção
- [ ] CSRF proteção em rotas de refresh/logout
- [ ] Logout com revogação + limpeza de cookies

## Migração do estado atual
1) Criar realm/cliente no Keycloak conforme acima.
2) Back-end: configurar JwtBearer; remover validações frouxas; exigir audience.
3) Implementar endpoints `/auth/callback`, `/auth/refresh`, `/auth/logout`.
4) SPA: implementar PKCE; guardar access token em memória; usar endpoints do back-end.
5) CI/CD: configurar variáveis de ambiente em Render/Vercel e remover segredos do código.
