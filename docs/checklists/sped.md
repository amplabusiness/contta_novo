# Checklist — SPED (EFD ICMS/IPI, EFD Contrib., ECD/ECF)

- [ ] EFD ICMS/IPI: C100/C170 somando com E110 (débitos/créditos)?
- [ ] IPI: C170 → E510 por CFOP/CST fecha?
- [ ] DIFAL/FCP: C101 por UF conciliado com E300/E310?
- [ ] Inventário: H010/quantidades e valores coerentes com H005 e ECD?
- [ ] CIAP: G110 apropriação vs G126 transportes?
- [ ] EFD-Contribuições: F100/F500/F550 e M100/M200/M210/M500/M600/M610 coerentes?
- [ ] NAT_BC_CRED/COD_CTA presentes e válidos (cross com ECD plano de contas)?
- [ ] ECD/ECF: períodos, CNPJ, UF e contas batendo com EFDs?

## Autenticação e Cadastros (referência rápida)

- Login de usuário
	- Endpoint: POST /api/access/getaccesstoken
	- Fluxo: AccessController → IUserApplication.GenerateAccessToken(email, senha)
	- Saída: JSON com { token, user }
	- Token: JWT Bearer validado via AddAuthentication(JwtBearer) no Startup; ConvertTokenId lê claims (nameid, exp).

- Onde fica a lista/validação de tokens?
	- Não há persistência de lista/blacklist neste repositório; a validação é stateless (JWT). Em produção, recomenda-se Keycloak OIDC e validação de issuer/audience/keys.

- Cadastro de empresa
	- Endpoint: POST /api/company/newcompany
	- Fluxo: CompanyController.NewCompany → valida CompanyValidator → MediatR → CompanyRepository (Mongo)
	- Consultas auxiliares: GET /api/company/getinfomation, GET /api/company/getall/{token}
