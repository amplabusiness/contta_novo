# Rodando local para testar login e cadastro (Keycloak + APIs + Website)

Pré-requisitos
- Docker Desktop (Windows)
- PowerShell ou WSL

Subir tudo com Docker
1) Na raiz do repo:
   - docker compose up -d --build

2) Acessos:
   - Keycloak: http://localhost:8080 (admin/admin)
   - Mailhog (e-mails de verificação): http://localhost:8025
   - Search API health: http://localhost:5001/health
   - Excel Parser health: http://localhost:5002/health
   - Website: http://localhost:3000

Fluxo de teste (login + convite por e-mail)
1) Login no portal:
   - Usuário seed: sergio@amplabusiness.com.br / senha: ChangeMe123!
   - Realm: contta (PKCE, OIDC)

2) Gerar convite (admin):
   - Obtenha um token de usuário admin via login no portal, chame a rota:
     POST http://localhost:5001/api/admin/invite
     Body: { "email": "novo@usuario.com.br", "roles": ["gestor"] }
   - Verifique o e-mail em: http://localhost:8025

Observações
- Variáveis do website ajustadas no docker-compose para usar Keycloak local.
- Usuário seed tem role "admin" para permitir o fluxo /api/admin/invite.
- RabbitMQ e MongoDB sobem para suportar o ConsumerXml (opcional para o login).
