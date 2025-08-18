## C) Tabelas Tributárias e Atualizações

Checklist de cobertura:
- Onde vivem as tabelas? — Done
- Rotina de atualização programada? — Done
- Como registra vigência/UF/exceções? — Done (lacunas apontadas)
- Logs/versionamento/rollback? — Done (lacunas e sugestões)

### Onde as tabelas tributárias vivem hoje

- CFOP (listas de referência)
  - Local: `Corporate.Contta.Schedule.Domain/Entities/ImpostoAgg/Cfop.cs` (classe `GetCfop`)
  - Tipo: listas estáticas hardcoded em código (múltiplos métodos: Venda, Entradas, Devolução, Transferência, etc.)
  - Observação: namespace usa o typo `Imporsto`.

- CSOSN (Simples Nacional)
  - Local: `Corporate.Contta.Schedule.Domain/Entities/ImpostoAgg/CsosnSimples.cs`
  - Tipo: lista estática hardcoded (códigos CSOSN permitidos)
  - Observação: também no namespace `Imporsto`.

- Anexos Simples (faixas, alíquotas e repartição IRPJ/CSLL/COFINS/PIS/CPP/ICMS)
  - Local: `Corporate.Contta.Schedule.Infra/Tools/CrawlerAliquota.cs` + modelo `Domain/Entities/NfeAgg/TabelaExterna.cs`
  - Tipo: scraping on-demand do HTML via AngleSharp; retorna `List<TabelaExterna>` em memória
  - Persistência: não há persistência nem coleção/arquivo dedicada (somente retorno no endpoint).

- NCM (catálogo)
  - Indício: `Corporate.Contta.Schedule.Domain/Entities/ImpostoAgg/TabelaNcm.cs` (duplicata em pasta “(1)”), com campos `Id`, `Ncm`, `Descricao`.
  - Uso: não há repositório/serviço visível consumindo esta tabela no código aberto aqui; o controller referencia um `CrawlerConsultaNcm` (não encontrado no repo), sugerindo intenção de popular/consultar NCM externamente.

- Códigos de Serviço (não exatamente tributo, mas referência fiscal)
  - Acesso: `NfeRepository.GetAllCodServico()` lê `TbCodServico` (Mongo)
  - Indica a existência de uma coleção Mongo para códigos de serviço (`TbCodServico`).

- ICMS ST / CEST / MVA por UF
  - Não foram encontradas tabelas ou repositórios com estes conteúdos nesta base (buscas por IcmsSt/CEST/MVA/Substituição Trib. não retornaram implementações).

### Existe rotina de atualização programada?

- Não há `BackgroundService`, `IHostedService`, Hangfire/Quartz ou cron configurados no código.
- Atualização de Anexos Simples é manual/on-demand via endpoint HTTP:
  - Controller: `Corporate.Contta.Schedule.Api/Controllers/NfeController.cs`
  - Rota: `GET /api/nfe/tabelaAnexo` — chama `CrawlerAliquota` (ou `CrawlerConsultaNcm`, que não está no repo) e retorna o resultado.
- Conclusão: não existe job/worker agendado para atualizar e persistir tabelas tributárias.

### Vigência, UF e exceções (NCM/CFOP/segmento)

- Vigência (data início/fim):
  - Não há modelagem de vigência nas estruturas atuais (`TabelaExterna` não possui campos de período; listas CFOP são estáticas).
- UF:
  - Não há segregação por UF para MVA/ICMS ST/benefícios nas tabelas; CFOP é nacional (lista única), Anexos Simples são federais sem UF na estrutura.
- Exceções por NCM/CFOP/segmento:
  - Não há mapeamentos ou tabelas de exceções por NCM/segmento.
  - Flags fiscais em `Produtos` (monofásico/benefício/isenção…) não são derivadas de tabelas; permanecem default/false após parse do XML.

### Logs, versionamento e rollback

- Logs: a API usa Serilog (controllers), porém não há trilhas específicas de "atualização de tabela tributária" (porque a atualização não é persistida).
- Versionamento: inexistente. Não há coleção/campo para `version`, `source`, `hash`, `effectiveFrom/effectiveTo` nas tabelas.
- Rollback: não existem funções de rollback, pois não há registros versionados nem snapshots de tabelas.

### Recomendações (próximos passos práticos)

1) Persistência e histórico
- Criar coleções em MongoDB com schema mínimo:
  - `TaxTableVersions` (generic): `{ _id, table, version, sourceUrl, fetchedAt, hash, notes }`
  - `SimplesAnnexRates`: `{ _id, annex, faixa, valorInicial, valorFinal, aliquota, reparticao:{IRPJ,CSLL,COFINS,PIS,CPP,ICMS}, effectiveFrom, effectiveTo, version }`
  - `NcmCatalog`: `{ _id, ncm, descricao, cest?, segmento?, effectiveFrom, effectiveTo, version }`
  - `IcmsStMvaUf`: `{ _id, uf, ncm, mva, cests?, exceptions, effectiveFrom, effectiveTo, source, version }`
  - `CfopMap`: `{ _id, cfop, tipo, descricao, effectiveFrom, effectiveTo, version }`

2) Atualização programada
- Implementar um `BackgroundService` para crawling + persistência ou configurar um cron no provedor (ex.: Render Cron ou GitHub Actions agendado) que chame um endpoint de atualização.
- Registrar logs por atualização (Serilog) e gravar `TaxTableVersions` com `hash` para detectar mudanças reais.

3) Vigência/UF/exceções
- Adicionar campos `effectiveFrom/effectiveTo`, `uf`, `exceptions` nas coleções mencionadas.
- Popular/atualizar a partir de fontes oficiais (ex.: Portais SEFAZ para ICMS ST/MVA e tabela NCM/CEST) no pipeline do crawler.

4) Rollback
- Expor endpoint “rollback de tabela” que altera o apontamento de `activeVersion` para uma versão anterior em `TaxTableVersions` (sem apagar dados).
- Padrão: soft-rollback por versionamento e feature flag `activeVersion` nas consultas.

5) Integração no cálculo
- Consumir as tabelas versionadas nas rotas de cálculo/apuração em vez de listas hardcoded (substituir `GetCfop` e preencher flags de `Produtos`).

### referências de código
- CFOP: `Corporate.Contta.Schedule.Domain/Entities/ImpostoAgg/Cfop.cs`
- CSOSN: `Corporate.Contta.Schedule.Domain/Entities/ImpostoAgg/CsosnSimples.cs`
- Crawler Anexos: `Corporate.Contta.Schedule.Infra/Tools/CrawlerAliquota.cs`
- Modelo Anexos: `Corporate.Contta.Schedule.Domain/Entities/NfeAgg/TabelaExterna.cs`
- Endpoint scraping: `Corporate.Contta.Schedule.Api/Controllers/NfeController.cs` (rota `tabelaAnexo`)
- TbCodServico: leitura via `NfeRepository.GetAllCodServico()`
