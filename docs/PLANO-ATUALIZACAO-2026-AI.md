# Plano de Atualização 2026 + Agentes de IA (Contta)

Este documento resume o estado atual, a arquitetura alvo, um roadmap até 2026 (Reforma Tributária) e orientações práticas de deploy. É um guia vivo para retomarmos os trabalhos.

## 1) Resumo executivo

- Objetivo: preparar a plataforma de apuração (Simples Nacional, Lucro Presumido, Lucro Real e SPED) para 2026, com regras atualizadas e agentes de IA para apoio em cálculo, conferência e explicação fiscal.
- Estratégia: estabilizar a base atual (auth, mensageria, APIs), separar regras de negócio em um “módulo de regras”, incluir RAG/IA para consulta de manuais/tabelas oficiais e criar agentes operacionais (classificação NCM/CFOP, conferência SPED, explicador de cálculo, assistente de apuração).
- Deploy: componentes gerenciados quando possível (MongoDB Atlas, CloudAMQP, Vercel) e containers onde conveniente (Keycloak, .NET consumer, APIs Node) em PaaS com Docker.

## 2) Estado atual (base funcionando)

- Auth: Keycloak (OIDC) com e‑mail (MailHog em dev). Portal preparado para OIDC. APIs Node (Search/Excel) validam JWT via JWKS.
- Mensageria: RabbitMQ com DLX/DLQ; consumidor .NET 8 robusto (ack/nack, QoS/prefetch, Serilog).
- Banco: MongoDB (containers em dev).
- Frontend: Portal CRA + Website Next.js (Vercel‑ready).
- Observabilidade inicial: logs/health endpoints.

## Tecnologias utilizadas (atual)

- Linguagens e frameworks
  - Node.js 18 LTS + TypeScript (APIs: Search/Excel)
  - .NET 8 (ConsumerXml)
  - React (CRA – Portal) e Next.js (Website)
- Autenticação e segurança
  - Keycloak 24 (OIDC/OAuth2, PKCE)
  - Portal: react-oidc-context
  - APIs: jose (JWT/JWKS)
- Mensageria
  - RabbitMQ 3.13 (management) com DLX/DLQ
- Banco de dados
  - MongoDB 6 (dev) → recomendado MongoDB Atlas em produção
  - ODM: Mongoose 6
- Build e ferramentas
  - Babel (transpilação TS), ts-node-dev (dev), ESLint/Prettier, Husky
- Observabilidade e saúde
  - Serilog (.NET), morgan (Node), endpoints /health
- E‑mail (dev)
  - MailHog (SMTP/UI)
- Containers e orquestração
  - Docker e docker-compose
- Dependências notáveis (Node)
  - express, cors, dotenv, mongoose, morgan, jose

## 3) Metas 2025 → 2026

- Confiabilidade: CI/CD, ambientes (dev/staging/prod), testes automatizados de regras e integrações.
- Regras 2026: parametrização por período/regime, feature flags para novas alíquotas e cenários de dupla apuração (comparativa).
- IA aplicável: classificação fiscal, conferência SPED, explicação de cálculo e assistente guiado (com trilhas de auditoria e aprovação humana).
- Compliance: LGPD, segurança, backups, trilhas de auditoria e retenção.

## 4) Arquitetura alvo (proposta prática)

- Frontend
  - Portal (CRA) e Site (Next.js). Hospedar Portal e Site no Vercel.
  - OIDC (Keycloak) com PKCE.
- APIs / Serviços
  - Node (TypeScript): Search API (NCM/CFOP/…); Excel Parser; novo Orchestrator de IA.
  - .NET 8: ConsumerXml (RabbitMQ).
  - Gateways/middlewares com validação JWT (jose) e papéis (realm roles: admin/gestor/operador/leitura).
- Mensageria
  - RabbitMQ (CloudAMQP gerenciado em produção; Docker local no dev).
- Banco de dados
  - Primário operacional: MongoDB Atlas (compatível com código atual).
  - Opcional/analítico: Postgres (Supabase) para relatórios analíticos e auditoria estruturada.
  - Vetores para RAG: MongoDB Atlas Vector Search (evita outra stack) ou pgvector (Supabase) se optar por Postgres.
- Autenticação/Identidade
  - Keycloak (realms/clients/roles); e‑mails transacionais via provedor (Resend/Mailgun/SES). Em dev: MailHog.
- Armazenamento de arquivos
  - S3‑compatível (Backblaze B2, AWS S3) para documentos anexos, planilhas e artefatos.
- Observabilidade e Segurança
  - Logs centralizados e alertas (Sentry/Datadog ou stack Loki/Promtail+Grafana). Rate limiting, CORS rígido, secrets via vault/managed secrets.

## 5) Agentes de IA (com OpenAI/ChatGPT API)

- Padrões e componentes
  - Orquestrador Node (TypeScript) com ferramentas (tools) seguras: acesso a NCM/CFOP, consulta a repositório de regras, leitura de documentos XML/JSON, validações SPED, escrita de relatórios.
  - RAG (Retrieval‑Augmented Generation) sobre manuais/tabelas/legislação atualizada.
  - Memória transacional por processo (contexto) e trilha de auditoria (log estruturado + persistência das decisões).
  - Human‑in‑the‑loop: toda decisão relevante exige aprovação/dupla checagem.
- Agentes sugeridos (MVP)
  1) Classificador Fiscal: sugere NCM/CFOP, exibe fundamentos normativos e alternativas.
  2) Conferência SPED: varre arquivos, marca inconsistências e produz relatório com “critérios e fontes”.
  3) Explicador de Cálculo: lê parâmetros (regime, período, receitas, créditos), gera cálculo passo‑a‑passo e justificativas.
  4) Assistente de Apuração: guia o usuário, coleta faltantes, aplica regras, pede confirmação em pontos de decisão.
- Tech stack IA
  - OpenAI API (gpt‑4o, o4‑mini ou modelos atualizados em 2025), function calling, JSON mode, streaming.
  - Biblioteca: LangChainJS ou “orquestração leve” própria (evita lock‑in, maior controle).
  - Vetor: Atlas Vector Search (Mongo) para documentos e normas; ETL de atualizações periódicas.
- Segurança/Compliance
  - Sanitização de entradas, red teaming básico, limites de escopo. Registro do prompt/inputs/outputs e justificativas. Desativar dados sensíveis no prompt (PII) ou mascarar. Retenção controlada (LGPD).

## 6) Reforma Tributária 2026 – estratégia de migração

- Feature flags por regime e por período (antes/depois da vigência).
- Engine de regras “data‑driven” (JSON/YAML) versionada; testes unitários e de regressão por cenário fiscal.
- Dupla apuração (comparativos) durante transição; relatórios de diferenças e justificativas.
- Conjunto de testes automatizados por “cesta” (Simples, Presumido, Real, SPED) com casos reais anonimizados.

## 7) Roadmap (fases)

- Fase 1 — Fundamentos (2–4 semanas)
  - CI/CD (GitHub Actions). Ambientes dev/staging/prod. Secrets e variáveis por ambiente.
  - Módulo de Regras (pacote TS/NET) extraindo cálculos do código atual.
  - Observabilidade (Sentry/Datadog) + backups automáticos (Atlas snapshots).
- Fase 2 — IA (4–6 semanas)
  - RAG: ingestão dos manuais/tabelas oficiais (pipeline + indexação vetorial).
  - Agentes MVP (classificador e conferência SPED) com API /orchestrator e UI simples no Portal.
  - Auditoria/explicabilidade: logs estruturados e “exportar relatório”.
- Fase 3 — Reforma 2026 (6–10 semanas)
  - Parametrização de regras 2026 + feature flags. Dupla apuração e comparativos.
  - Testes extensivos e validação com amostras reais; ajustes finos de performance e custos.
- Fase 4 — Operação (contínua)
  - Hardening segurança (CSP, rate limit, WAF do provedor). Plano de desastre, RTO/RPO. FinOps (monitorar custo IA e DB).

## 8) Deploy recomendado (produção)

- Frontend (Portal e Site): Vercel (build automático por branch/tag, variáveis de ambiente seguras).
- APIs Node (Search/Excel/Orchestrator): Render ou Railway (deploy via Dockerfile). Alternativa: Fly.io.
- Consumer .NET 8: Fly.io (container) ou Azure App Service (se preferir Azure).
- Keycloak: Fly.io (container com volume) ou instância gerenciada (se disponível).
- RabbitMQ: CloudAMQP (gerenciado) — reduz operação.
- Banco de Dados: MongoDB Atlas (principal). Opcional: Supabase (Postgres) para relatórios/analíticas.
- Armazenamento: S3 compatível (Backblaze B2 / AWS S3) com versionamento.

## 9) Custos e contratação (visão prática)

- Minimizar operação: usar Atlas (Mongo), CloudAMQP, Vercel, Render/Railway e S3. Todos com camadas de entrada acessíveis e upgrade sob demanda.
- IA: controlar gastos com limites por projeto/chave, cache de respostas e RAG eficiente para reduzir tokens.

## 10) Próximos passos operacionais

1) Criar contas/provisões: Vercel, MongoDB Atlas, CloudAMQP, Render/Railway, Backblaze B2 ou AWS.
2) Gerar variáveis de ambiente por ambiente (dev/staging/prod) e segredos (JWT audience/issuer, OpenAI keys, conexões Atlas/AMQP).
3) Configurar CI/CD (GitHub Actions) com deploy automático.
4) Extrair e modularizar Regras atuais (baseline) + testes.
5) Subir Orchestrator de IA (Node) + ingestão RAG (docs oficiais).
6) Implementar Agentes MVP e telas no Portal (explicações, aprovações).

## 11) Variáveis de ambiente (exemplos)

- Geral (APIs)
  - OIDC_ISSUER=https://<KEYCLOAK>/realms/contta
  - OIDC_AUDIENCE=contta-portal
  - MONGODB_URI=mongodb+srv://...
  - RABBITMQ_URL=amqps://<user>:<pass>@<cloudamqp-host>/<vhost>
  - OPENAI_API_KEY=sk-...
- Keycloak
  - Admin user/password; SMTP (provedor real em prod).
- Portal (Vercel)
  - REACT_APP_AUTH_MODE=oidc
  - REACT_APP_OIDC_AUTHORITY=https://<KEYCLOAK>/realms/contta
  - REACT_APP_OIDC_CLIENT_ID=contta-portal
  - REACT_APP_SEARCH_API_BASE_URL=https://api.conta.com.br
  - REACT_APP_EXCEL_API_BASE_URL=https://excel.conta.com.br

## 12) Riscos e controles

- LGPD e PII: mascaramento, retenção, consentimento e base legal. Auditoria dos acessos e decisões.
- Mudanças 2026: cronograma oficial, flags por período e fallback para regras antigas.
- Dependências externas: monitoramento de disponibilidade (DB, AMQP, OpenAI) e circuit breakers.
- Custos IA: limites, caching, batch, e modelos mais econômicos para tarefas adequadas.

---

Este plano prioriza o que é executável com baixo atrito e alto impacto. A próxima sessão deve começar por: provisionamento dos serviços gerenciados, CI/CD, extração do módulo de regras e criação do Orchestrator de IA com RAG mínimo.
