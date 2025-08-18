# Integração com API

## Cliente HTTP
- Lib atual: fetch (apenas no fluxo OIDC em `/auth/callback`)
- Base URL por ambiente: sugerir `NEXT_PUBLIC_API_URL` (definida em `.env.example` comentada)
- Interceptors: inexistentes (sugestão de futuro `api.ts` com axios e interceptors)

## Arquivos/Linhas (atuais)
- Token exchange: `src/pages/auth/callback.tsx` (POST para `${issuer}/protocol/openid-connect/token`)

## Tabela de endpoints consumidos (atual)
| Módulo | Método | Path | Request | Response | Tratamento de erro | Observações |
|---|---|---|---|---|---|---|
| Auth | POST | `${issuer}/protocol/openid-connect/token` | x-www-form-urlencoded | JSON tokens | Verifica `resp.ok` e mostra texto | Apenas no callback OIDC |

## Sugestão (evolução)
- Criar `src/lib/api.ts` com axios + interceptors:
	- Request: injeta `Authorization: Bearer ${sessionStorage.getItem('access_token')}`
	- Response: trata 401 e redireciona para `/login` ou implementa refresh token (se habilitado)
