# Trilhas de Execução — Motor de Cálculo (endpoint → persist)

Fluxo geral com base nos controllers e repositórios mapeados.

- Autenticação
  - POST /api/access/getaccesstoken
    - AccessController.Login → IUserApplication.GenerateAccessToken(email, senha)
    - Retorna: { token, user }

- Empresa (cadastro e faturamento)
  - GET /api/company/getinfomation → MediatR(GetCompanyRequest) → CompanyRepository
  - GET /api/company/getall/{token} → ConvertTokenId.GetTokenUserMaster(token) → MediatR(GetAllCompanyRequest)
  - PUT /api/company/faturamento (fechado=false) → CompanyRepository.UpdateFaturamentoEmpre
  - POST /api/company/newfaturamento12 → CompanyRepository.NewFaturamentoEmp

- NF-e/NFS-e
  - GET /api/nfe/getall → MediatR(GetAllNfeRequest) → NfeRepository
  - GET /api/nfe/consultanfe → MediatR(FilterNfeRequest) → NfeRepository
  - GET /api/nfe/getallNfeLivro → NfeRepository.GetAllNfe → WebGerarPDF.GerarRelatorioSaida/Entrada
  - GET /api/nfe/getallTbE → NfeRepository.GetAllBlocoE
  - POST /api/nfe/ajusteBlocoE → MediatR(NewAjusteNfeRequest) → NfeRepository.InsertTbAjuste

- Impostos/CFOP
  - GET /api/impostos/getall → MediatR(GetAllProdutosImpostosRequest) → ImpostosProdutosRepository
  - GET /api/impostos/getallCfop, getallCfopsm → ImpostosProdutosRepository.GetAllCfop*
  - POST /api/impostos/newCfop → ImpostosProdutosRepository.NewCfop
  - DELETE /api/impostos/deleteCfop → ImpostosProdutosRepository.DeleteCfop

- Apuração ICMS/IPI (Bloco E)
  - POST /api/agblocoe (AgBlocoERequest) → MediatR → Repositórios de apuração
  - GET /api/nfe/getallapuracao (ApuracaoRequest) → MediatR → Regras/Agregações
  - GET /api/nfe/GetRegistrosBlE → MediatR(GetRegistrosBlERequest)

- Crawlers/Tabelas
  - GET /api/nfe/tabelaAnexo → CrawlerConsultaNcm (ou CrawlerAliquota)

- Persistência
  - Infra.Mongo: Repositórios (NfeRepository, CompanyRepository, ImpostosRepository ...)
  - Documentos: NFE, Itens, Tabelas, Apurações, Configurações

Observação: Handlers MediatR não estão visíveis neste recorte; são invocados via _mediator.Send(request) a partir dos controllers.
