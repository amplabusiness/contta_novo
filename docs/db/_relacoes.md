# Relações (MongoDB) e diagrama ER

MongoDB não impõe FKs; relações são lógicas. Use o diagrama Mermaid em `er_diagrama.md` para visualizar cardinalidades previstas.

- NFE 1--* Produtos (campo: Produtos.NfeId → NFE.Id)
- NFE 1--* ProdutosFornecedor (ProdutosFornecedor.NfeId → NFE.Id)
- NFE *--1 EmpresaDest (NFE.EmpresaDesId → EmpresaDest.Id)
- NFE *--1 CompanyInformation (NFE.CompanyInformation → CompanyInformation.Id)
- ServicoEntity *--1 CompanyInformation (ServicoEntity.CompanyInformation → CompanyInformation.Id)
- TbDashboardClientes *--1 CompanyInformation (TbDashboardClientes.CompanyInformation → CompanyInformation.Id)
- AjusteNfe *--1 CompanyInformation (AjusteNfe.CompanyInformation → CompanyInformation.Id)
