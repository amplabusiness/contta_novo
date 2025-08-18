# Índices e Performance (MongoDB)

Levantamento inicial (a confirmar via `db.getCollectionInfos()` e `db.collection.getIndexes()` em produção/dev):

- NFE
  - Provável índice por `CompanyInformation`, `ModeloTipo`, `DhEmi` (consultas por mês e operação)
  - Recomendação: `{"CompanyInformation":1, "ModeloTipo":1, "DhEmi":1, "Modelo":1}` com cobertura para filtros mais comuns
- Produtos
  - `NfeId` para join aplicação
- EmpresaDest
  - `Cnpj` (busca por documento)
- TbDashboardClientes
  - `CompanyInformation`, `DataEmissaoMensais.Ano`, `DataEmissaoMensais.Mes`

Adicione a seção “índices reais” após exportar `db.collection.getIndexes()` e anexar em `docs/db/export/`.
