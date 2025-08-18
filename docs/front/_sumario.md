# Sumário Executivo do Frontend

## Visão geral
- Framework: Next.js 14 (`contta-website-main/contta-website-main/package.json`, dep "next": "^14.2.5") + React 18 ("react": "^18.2.0") + styled-components 5 ("styled-components": "^5.0.0")
- Linguagem: TypeScript ("typescript": "^4.1.3" em devDependencies; `contta-website-main/contta-website-main/tsconfig.json`)
- Padrão de rotas: pages/ (pasta `contta-website-main/contta-website-main/src/pages` com `index.tsx`, `_app.tsx`, `_document.tsx`, `login.tsx`, `auth/callback.tsx`)
- Bibliotecas UI: styled-components; react-icons ("react-icons": "^4.1.0"). Não há Tailwind/MUI/shadcn.
- Estado & Dados: Sem libs globais detectadas (sem Redux/Zustand/React Query). Contexto local para UI: `src/context/ui`.
- Animações & Ícones: Ícones via `react-icons`. Não há Framer Motion.
- Autenticação: Keycloak OIDC + PKCE (`src/pages/login.tsx` linhas ~4-22; `src/pages/auth/callback.tsx` linhas ~3-36; `src/utils/pkce.ts`)
- Cliente HTTP: fetch nativo no callback OIDC (`src/pages/auth/callback.tsx` linhas ~26-34). Nenhum axios/interceptor no projeto.

## Arquivos de configuração (citar caminho exato + linha)
- `contta-website-main/contta-website-main/package.json` → scripts: "dev","build","start"; deps Next/React; styled-components
- `contta-website-main/contta-website-main/next.config.js` → compiler.styledComponents=true; eslint/typescript ignoreDuringBuilds=true
- `contta-website-main/contta-website-main/tsconfig.json` → jsx: preserve; module: esnext
- `contta-website-main/contta-website-main/.env.example` → NEXT_PUBLIC_OIDC_ISSUER, NEXT_PUBLIC_OIDC_CLIENT_ID, NEXT_PUBLIC_API_URL (comentadas)

## Módulos do front
- Layout: `src/components/Layout/*`, `src/components/Header/*`, `src/components/Footer/*`, `src/components/VideoOverlay/*`
- Autenticação: páginas `/login` e `/auth/callback`; util PKCE em `src/utils/pkce.ts`
- Navegação: páginas em `src/pages`; âncoras com react-scroll em Header e Sections
- UI Kit: tokens em `src/styles/theme.ts` e globais em `src/styles/global.ts`
- Páginas chave: Home (`/` com Sections: Landing, OurGoal, Services, Contacts). Não há dashboard/notas/apuração neste app.

## Riscos & quick wins
- Riscos:
	- [x] Tokens não padronizados / cores hardcoded (há cores inline em vários componentes)
	- [ ] Interceptor JWT inconsistente (não existe cliente central HTTP, risco ao escalar)
	- [ ] Falta de testes visuais/a11y
- Quick wins (≤7 dias):
	- [x] Extrair tokens para `docs/front/tokens.json` (feito)
	- [ ] Normalizar header/spacing/sombras via theme
	- [ ] Storybook mínimo (Button, Card)
