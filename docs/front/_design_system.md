# Design System (Tokens + Componentes)

## 1) Tokens (origem)
- Cores: definidas em `contta-website-main/contta-website-main/src/styles/theme.ts`
	- primaryColor: #3276b1
	- headingColor: #333333
	- textColor: #6a6870
	- ligthTextColor: #ffffff
- Tipografia: `global.ts` aplica Rubik (via `_document.tsx`)
- Espaçamentos, raios, sombras, z-index: não há tokens formais; definidos inline em componentes

> Snapshot exportado em `docs/front/tokens.json` (cores + fonte).

## 2) Catálogo de componentes (base)
| Componente | Caminho | Props principais | Variantes/estados | Dependências | Notas |
|---|---|---|---|---|---|
| Layout | `src/components/Layout/*` | children | - | styled-components | Wrapper base |
| Header | `src/components/Header/*` | scrolled | estado por scroll | react-scroll | Usa logo |
| Sections | `src/components/Sections/*` | - | - | react-icons | Landing/Services/Goal/Contacts |
| Footer | `src/components/Footer/*` | - | - | - | Rodapé |
| VideoOverlay | `src/components/VideoOverlay/*` | open/close | overlay | - | Controlado por UI context |

## 3) Padrões de uso
- Botões: estilizados inline nos componentes; considerar extrair para Button base
- Formulários: não há
- Tabelas: não há
- Responsivo: media queries locais em styled-components (ver `Header/styles.ts`)
