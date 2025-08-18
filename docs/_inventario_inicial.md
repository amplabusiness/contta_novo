# Inventário inicial do repositório

Este inventário resume a estrutura detectada e aponta artefatos principais para auditoria e deploy.

## Raiz
- docker-compose.yml
- render.yaml (blueprint de múltiplos serviços)
- README.md, README-RUN.md, SYSTEM_INVENTORY.md
- scripts/setup-rabbitmq.ps1

## Back-end (agendador-back-end-master)
- Solution: `Corporate.Contta.Schedule.Service.sln`
- Projetos: Api, Application, Domain, Infra, SpedContta, UnitTest, WebGerarPDF
- Pastas relevantes: `ConsumerXml/`, `ConsumerXml.Tests/`

## Portais/Fronts
- `portal-contta-master/`
- `portal-simples-front-end-master/`
- `contta-simples-master/`
- `contta-website-main/`

## Serviços auxiliares
- `contta-excel-parser-main/` (Node/TS)
- `contta-search-api-main/` (Node/TS)
- `crawler-econet-main/`
- `recupera-pis-cofins-main/`
- `cnpj-main/`
- `auth-ecac-main/`
- `robo-eco-master/`
- `downloader-xml-sefaz-go-main/`
- `bak-contta-main/`, `emalotedescktop-master/`

## Observações
- Múltiplos repositórios empacotados na mesma raiz.
- Segredos devem ser migrados para Secret Stores (Render, Vercel, GitHub).
- Conexões MongoDB hardcoded localizadas em Infra do backend.

## Como regenerar localmente
- Abra um PowerShell na raiz do repo. Ex.: `...\contta_novo\`
- Execute: `./scripts/generate-inventory.ps1`
- O script coleta: `dotnet --info`, `dotnet list package --include-transitive`, e um sumário de pastas/soluções e persiste neste arquivo.
- Certifique-se de estar na raiz do repositório ao executar o script.
