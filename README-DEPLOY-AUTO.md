# Deploy Automatizado (Render + GitHub Actions)

Este repositório inclui um workflow que dispara o Render Blueprint sem scripts locais.

## Como configurar
1. No GitHub do projeto: Settings > Secrets and variables > Actions > New repository secret
   - Nome: `RENDER_API_TOKEN`
   - Valor: seu token da Render (rnd_...)
2. (Opcional) Garanta no Render:
   - `MONGODB_URI` setada nas APIs
   - `RABBITMQ_URL` setada no Consumer (se não for passar no input)
   - `CORS_ORIGINS`/`PRODUCTION_URL` conforme seu front (Vercel)

## Como executar
- GitHub > Actions > "Render Blueprint Deploy" > Run workflow
  - Inputs opcionais:
    - `mongodb_uri`: se quiser sobrescrever
    - `rabbitmq_url`: amqps://... (CloudAMQP)
    - `cors_origins`: ex: `*.vercel.app,https://e-contta.com.br`
    - `production_url`: ex: `https://e-contta.com.br`

O job monta `payload.json` e chama `POST https://api.render.com/v1/blueprints`.

## Verificação
- Acompanhe o resultado no log do workflow (Status HTTP e resposta).
- No Render, verifique os deploys e `/health` das APIs.

## Observações
- Vercel: configure `NEXT_PUBLIC_SEARCH_API_URL` e `NEXT_PUBLIC_EXCEL_API_URL` com as URLs das APIs do Render.
- Segurança: se expôs a URL do CloudAMQP, rotacione a senha no painel após concluir.
