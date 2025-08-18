# Uso de CFOP e NCM nas Regras

## CFOP
- `Domain/Entities/ImpostoAgg/Cfop.cs`: listas estáticas por tipo de operação.
- `Infra/Repositories/NfeRepository.cs`:
  - `GetAllNfe(...)` e geração de Livro Fiscal: agrega/agrupa por CFOP; rodapés por CFOP.
  - `InsertAjusteNfe(...)`: filtra por `ajusteNfe.Cfops` e soma valores.
- `Infra/Models/TbSimplesNacional/IntegrationTbSimplesNfManual.cs`:
  - Soma BaseCalculo do Simples se CFOP do item consta na coleção `tbCFOP` (Mongo).

## NCM
- Armazenado em `Produtos.NcmProd`.
- Crawler: `Infra/Tools/CrawlerConsultaNcm.cs` (retorna lista em memória).
- Regras por NCM (monofásicos/ST): não localizadas na lógica atual.

## Diferenças e sugestões
- CFOP: efetivamente usado para base e relatórios.
- NCM: disponível mas sem regras ativas. Sugerir tabelas versionadas (NCM→regra) e aplicação no pipeline (Adapter/Repos).
