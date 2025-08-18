# E) Simples Nacional — alíquota efetiva e partilha

Este inventário descreve o que existe hoje no código sobre Simples Nacional e o que falta para calcular alíquota efetiva e partilha do DAS (IRPJ, CSLL, PIS, COFINS, CPP e ICMS/ISS).

## Checklist de cobertura
- [x] Módulo do Simples identificado
- [x] Segregação de receitas (por anexo/atividade) — como é hoje e lacunas
- [x] Tratamento de monofásicos/ST/ex-tarifário — status atual
- [x] RBT12 — como é apurado hoje e lacunas
- [x] Fator R — status
- [x] Alíquota nominal e efetiva — onde buscar e o que falta
- [x] Repartição por tributo (DAS) — fonte e status de uso
- [x] ICMS/ISS dentro do DAS — status
- [x] Exemplos com referências de código
- [x] RoboEconet — o que faz, se funciona e lógica

---

## Onde está o “módulo do Simples” hoje
- Agregação mensal e base: `Corporate.Contta.Schedule.Infra/Models/TbSimplesNacional/IntegrationTbSimplesNfManual.cs`
  - Persiste/atualiza documentos `TbSimples` (classe `TbDashboardClientes`) por empresa e mês, somando faturamento e uma “BaseCalculo”.
- Estrutura do documento: `Corporate.Contta.Schedule.Domain/Entities/NfeAgg/TbDashboardClientes.cs`
  - Campos: `ValorContabil` (saídas/serviços/fretes + `BaseCalculo`), `SimplesNacional` (`FaturamentoAnual`, `BaseDeCalculo`, `ImpostosAPagar`, `DateFounded`).
- Cálculo fiscal formal (placeholder): `Corporate.Contta.Schedule.Domain/Entities/NfeAgg/CalcularSimples.cs`
  - Classe vazia (somente DTOs de impostos). Não há motor de cálculo de Simples implementado.

Conclusão: o “módulo” vigente é uma integração de faturamento/base (TbSimples). O motor de apuração do Simples (faixas, deduções, partilha) ainda não existe.

## Segregação de receitas (por Anexo/atividade)
- Hoje: não há segregação por Anexo. A integração soma:
  - Mercadorias (NFe): soma por itens com CFOP listado em `tbCFOP` → `BaseCalculo += VlProduto - VlTlDesconto`.
  - Serviços (NFS-e): soma o valor líquido do serviço em `BaseCalculo` e em indicadores de frete (intra/inter/interestadual).
  - Referências:
    - `IntegrationTbSimplesNfManual.CreateTbNfeSaidaManual` e `ValidacaoEntardaSaida` (mercadorias)
    - `IntegrationTbSimplesNfManual.CreateTbNfeServicoPrestador` e `ValidacaoTbSimplesServico` (serviços)
- Anexo por CNAE (consulta):
  - `Corporate.Contta.Schedule.Infra/Tools/CrawlerConsultaAnexo.cs` retorna uma string com o “anexo” a partir do CNAE numa página externa. Não é consumido para classificar receitas nem para cálculo.

Lacuna: não há classificação de receitas por Anexo (I–V/VI) nem por atividade (CNAE) nos totais usados para cálculo do DAS.

## Monofásicos, ST e ex-tarifário
- Flags existem nos cadastros/itens (ex.: `NcmMono`, `IcmsSt`, benefícios), porém:
  - Não são derivadas automaticamente do XML nem de tabelas auxiliares no pipeline atual.
  - Não há regra que exclua monofásicos da base do Simples (como deveria para PIS/COFINS, por exemplo) nem tratamento especial para ex-tarifário.
- Na agregação atual, a base de mercadorias usa somente `VlProduto - VlTlDesconto`. ST/IPI não entram explicitamente nessa soma; contudo não há regra formal garantindo exclusões por regime/benefício.

Lacuna: ausência de regras que ajustem base por monofásico, ST, ex-tarifário ou benefícios.

## RBT12 (Receita Bruta dos últimos 12 meses)
- O que há: leitura de um “faturamento anual” de outra coleção para preencher `SimplesNacional.FaturamentoAnual`:
  - `IntegrationTbSimplesNfManual.CriateFaturamento(...)` soma `faturamento12.Faturamentos.Sum(c => c.ValorFaturamento)` de `FaturamentoEmpresa` (coleção). O modelo dessa coleção não está no repo, mas o código pressupõe sua existência.
- O que falta: cálculo de RBT12 rolling (janelas móveis por competência). Não há rotina que consolide 12 meses corridos a partir de `TbSimples` nem integração com o faturamento para janelas móveis.

Lacuna: RBT12 não é apurado automaticamente no backend; apenas um “anual” agregado externo é lido (quando existe).

## Fator R
- Não há qualquer implementação de Fator R (folha/RBT12) nem campos de folha na estrutura `TbSimples`.

Lacuna: sem Fator R, não há migração dinâmica entre Anexo III/V para serviços conforme regra.

## Alíquota nominal e alíquota efetiva
- Fonte dos dados (nominal, dedução e partilha): crawler de anexos captura faixas, alíquotas, parcela a deduzir e repartição:
  - `Corporate.Contta.Schedule.Infra/Tools/CrawlerAliquota.cs` → retorna `List<TabelaExterna>` com: `Faixa, ValorInicial, ValorFinal, Aliquota, Deduzir, IRPJ, CSLL, Cofins, PISPasep, CPP, ICMS`.
  - Modelo: `Corporate.Contta.Schedule.Domain/Entities/NfeAgg/TabelaExterna.cs`.
- Uso atual: não há persistência nem aplicação destas tabelas para calcular o DAS. A rota que chama o crawler (GET `/api/nfe/tabelaAnexo`) devolve a lista em memória (sem versionamento/vigência).
- Fórmula esperada (não implementada hoje):
  - Alíquota efetiva = ((RBT12 × Alíquota nominal) − Parcela a deduzir) ÷ RBT12.
  - DAS mês = Alíquota efetiva × Receita segregada do mês (por anexo/atividade), com ajustes de exclusões (monofásico/ST etc.).

Lacuna: falta persistir as tabelas (por Anexo, vigência) e aplicar a fórmula com RBT12, dedução e segregaçāo correta.

## Repartição por tributo e ICMS/ISS dentro do DAS
- O crawler já traz os percentuais de repartição do DAS por tributo: IRPJ, CSLL, COFINS, PIS, CPP e ICMS (para anexos de comércio/indústria). Para serviços (Anexos III/V), o componente de tributo local é ISS, mas não há campo ISS no modelo atual; somente `ICMS` aparece em `TabelaExterna`.
- Status: a aplicação não consome esses percentuais para compor os valores por tributo; `SimplesNacional.ImpostosAPagar` nunca é calculado; não há gravação dos valores partilhados.

Lacuna: ausência de partilha por tributo e de componente ISS para serviços no modelo atual.

## Exemplos no código (referências)
- Base de mercadorias (produto) somada no mês:
  - `IntegrationTbSimplesNfManual.CreateTbNfeSaidaManual`: `baseCalculo += item.VlProduto - item.VlTlDesconto` quando `item.Cfop` aparece em `tbCFOP`.
  - `IntegrationTbSimplesNfManual.ValidacaoEntardaSaida`: acumula `ValorSaidaMercadoria` e soma `BaseCalculo` no documento mensal `TbSimples`.
- Base de serviços somada no mês:
  - `IntegrationTbSimplesNfManual.CreateTbNfeServicoPrestador`: define `NotaServicoPrestador` e classifica frete intra/inter/interestadual.
  - `IntegrationTbSimplesNfManual.ValidacaoTbSimplesServico`: `BaseCalculo += valorSaida` (líquido do serviço) em `TbSimples`.
- Tabela de anexos (nominal, dedução e partilha):
  - `CrawlerAliquota.GetAnexo(url)` → popula `TabelaExterna` com `Aliquota`, `Deduzir` e percentuais de `IRPJ/CSLL/COFINS/PIS/CPP/ICMS` por faixa.
- Anexo por CNAE (consulta indicativa, não integrada ao cálculo):
  - `CrawlerConsultaAnexo.GetAnexo(cnae)` → retorna string com o “Anexo” encontrado na página consultada.

Observação: não há uso dessas tabelas para calcular alíquota efetiva, nem segmentação por Anexo nas somas da base.

## RoboEconet — o que faz e lógica
- Objetivo: montar e persistir dataset fiscal (principalmente PIS/COFINS por NCM, com CSTs, observações e EFD) a partir de fonte externa (Econet) para consulta posterior.
- Componentes:
  - Service: `RoboEconet/Services/PisConfinsService.cs` → resolve `IPisConfinsRepository` via IoC e chama `Create(...)` com a lista DTO.
  - Adapter: `RoboEconet/Infra/Adapter/EntidadePisConfinsToEntidadeMongodb.cs` → converte modelos raspados (`PisConfins`) em `PisConfinsDto` agregando `NCMS`, `Aliquotas`, `CSTS`, `Observacoes`, `EFDS` e flag `Monofasico`.
  - Repository: `RoboEconet/Infra/Data/Repositorios/PisConfinsRepository.cs` → insere em MongoDB (evitando duplicidades por `NCMPai`).
  - Contexto Mongo: `RoboEconet/Infra/Data/Base/MongoDBContext.cs` → aponta por padrão para `mongodb://localhost:27017/` no DB `tbGeralImp` (hardcoded).
- “Funciona?”
  - Sim, como projeto separado para popular uma base Mongo local com dados de PIS/COFINS por NCM (inclui marcação de monofásico), desde que a origem (scraper) esteja operante e o Mongo local disponível.
  - Integração com o backend do agendador: não há uso direto dessas coleções no cálculo/segregação do Simples. Serve como fonte auxiliar para futuras regras.

## O que falta para calcular a alíquota efetiva de ponta a ponta
1) Persistir e versionar tabelas de Anexo (faixas, alíquota, dedução, partilha, vigência) — consumir o `CrawlerAliquota` para preencher coleções estáveis (por Anexo).
2) Apurar RBT12 (janela móvel) a partir do `TbSimples` ou de `FaturamentoEmpresa` e registrar no documento do mês.
3) Implementar estratégia de segregação por Anexo/atividade:
   - Serviços (CNAE) via `CrawlerConsultaAnexo` + cadastro (com override manual por atividade, quando necessário) e Fator R para migração Anexo III/V.
   - Mercadorias por natureza de receita (Anexos I/II) — parametrizar por CFOP/NCM/atividade.
4) Aplicar fórmula da alíquota efetiva (por Anexo) e partilhar o DAS por tributo, gravando no `TbSimples`:
   - `aliquotaEfetiva = ((RBT12 × aliquotaNominal) − deducao) ÷ RBT12`.
   - `valoresPorTributo = aliquotaEfetiva × baseSegregada × percentualPartilha` (por IRPJ, CSLL, PIS, COFINS, CPP, ICMS/ISS).
5) Regras de exclusão/ajuste: monofásicos, ST, ex-tarifário, benefícios (redução/isenção), com trilha de regra.
6) ISS: incluir componente de ISS na tabela/partilha dos Anexos de serviços e refletir na apuração.

## Riscos e melhorias rápidas
- Conexões Mongo hardcoded em `IntegrationTbSimplesNfManual` e outros repositórios — migrar para variáveis de ambiente/DI.
- Possível “duplo desconto” no fluxo manual de produtos (item `VlProduto` já vem descontado e a integração abate `VlTlDesconto` novamente) — revisar.
- Criar testes unitários mínimos: base de mercadorias/serviços, monofásico excluído, RBT12 e alíquota efetiva com dedução.

## Referências de arquivo
- `Corporate.Contta.Schedule.Infra/Models/TbSimplesNacional/IntegrationTbSimplesNfManual.cs`
- `Corporate.Contta.Schedule.Domain/Entities/NfeAgg/TbDashboardClientes.cs`
- `Corporate.Contta.Schedule.Domain/Entities/NfeAgg/CalcularSimples.cs`
- `Corporate.Contta.Schedule.Infra/Tools/CrawlerAliquota.cs`
- `Corporate.Contta.Schedule.Domain/Entities/NfeAgg/TabelaExterna.cs`
- `Corporate.Contta.Schedule.Infra/Tools/CrawlerConsultaAnexo.cs`
- `robo-eco-master/RoboEconet/*`

---

Status de cobertura: módulos mapeados e lacunas identificadas para implementar a alíquota efetiva e a partilha do DAS.
