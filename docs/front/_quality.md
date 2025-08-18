# Qualidade: Acessibilidade, Performance e Testes

## Acessibilidade (a11y)
- aria-* e foco visível: não padronizados; revisar Header/Sections
- Contraste: garantir AA para #3276b1 vs fundos claros
- Teclado: navegação por âncoras ok; verificar foco ao abrir overlays

## Performance
- Imagens: uso de `next/image` no Header/OurGoal; otimização padrão Next
- Code splitting: não há `dynamic()`; opcional para Sections pesadas
- Core Web Vitals: não medidos; executar Lighthouse

## Testes
- Unit: não há testes configurados
- E2E: sugerir Playwright para fluxo de login (PKCE) e navegação por âncoras
- Visuais/Storybook: não presente; sugerir iniciar com 2-3 componentes

## Build
- Scripts: `package.json` → dev/build/start
- `next.config.js`: `ignoreDuringBuilds: true` para ESLint/TS — revisar antes de PROD
- Dockerfile: multi-stage Node 18-alpine; build e `next start` em 3000

## Checklists
- [ ] Cores/tokens sem hardcode em componentes
- [ ] Botões/inputs com foco visível
- [ ] Cliente HTTP com interceptor (quando criado)
- [ ] Páginas pesadas com lazy/dynamic
- [ ] Tabela com paginação/empty states (quando aplicável)
