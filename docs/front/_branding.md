# Branding e Identidade Visual

## Logos & assets
| Arquivo | Caminho | Variação | Dimensão recomendada | Área de respiro | Observações |
|---|---|---|---|---|---|
| Logo full (PNG) | `contta-website-main/contta-website-main/public/images/logo.png` | positiva | 135×80 (usado no Header) | 1x | Referência: `src/components/Header/index.tsx` (prop do Next Image) |
| Favicon | `contta-website-main/contta-website-main/public/favicon.ico` | - | 32×32 | - | Linkado em `src/pages/_app.tsx` `<Head>` |

> Cópias para documentação podem ser mantidas em `/docs/front/assets/`.

## Tipografia
- Família: Rubik, importada via Google Fonts
  - Import/location: `contta-website-main/contta-website-main/src/pages/_document.tsx` dentro de `<Head>`
  - Aplicação global: `contta-website-main/contta-website-main/src/styles/global.ts` (body, button)
- Pesos usados: 300, 400, 700 (conforme URL do Google Fonts)
- Escala real (exemplos): 16px base; headings variam por componente (Sections)
- Line-height / tracking: herdado; não há tokens específicos definidos

## Paleta de cores (reais do projeto)
| Token | HEX | HSL | Uso | Onde definido |
|---|---|---|---|---|
| brand-primary | #3276b1 | - | Destaques, ícones, botões | `src/styles/theme.ts` colors.primaryColor |
| fg-heading | #333333 | - | Títulos | `src/styles/theme.ts` colors.headingColor |
| fg-text | #6a6870 | - | Texto padrão | `src/styles/theme.ts` colors.textColor |
| fg-light | #ffffff | - | Texto claro | `src/styles/theme.ts` colors.ligthTextColor |

## Regras de uso
- Logo mínima: manter legibilidade; altura ~40–56px em header responsivo
- Contraste: seguir WCAG AA especialmente para links/botões sobre fundos claros/escuros
- Dark mode: não implementado; caso necessário, criar tokens alternativos e alternância por classe `.dark`
