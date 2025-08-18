## Bill of Materials (BOM)

Dependências de runtime (`contta-website-main/contta-website-main/package.json`):
- next: ^14.2.5
- react: ^18.2.0
- react-dom: ^18.2.0
- styled-components: ^5.0.0
- react-icons: ^4.1.0
- react-is: ^16.8.0 (peer de styled-components)
- react-scroll: ^1.8.1

Dev deps:
- typescript: ^4.1.3
- eslint (+plugins), prettier
- @types/react, @types/node, @types/styled-components, @types/react-scroll

Assets:
- `public/images/logo.png`, `public/images/goal.svg`, `public/images/meet.jpg`

Build/Config:
- `next.config.js`: styledComponents compiler, ignoreDuringBuilds (eslint/ts)
- `tsconfig.json`: jsx preserve, module esnext
- `.env.example`: NEXT_PUBLIC_OIDC_ISSUER, NEXT_PUBLIC_OIDC_CLIENT_ID, NEXT_PUBLIC_API_URL
- `Dockerfile`: Node 18-alpine, build e `next start` (porta 3000)
