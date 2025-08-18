# Perguntas Abertas / Decisões Penduradas

- Qual será o Authority oficial (Issuer) do Keycloak em produção (domínio final)? Precisamos configurar `Authority` e `Audience` na API.
- Quais domínios exatos devem constar em CORS (portal, subdomínios de APIs, admin)? Mover para `CORS_ORIGINS` no Render.
- Confirmar estratégia de idempotência além de `CodBarra`: há cenários com reprocessamento de NF-e? Devemos registrar hash de XML?
- Qual a política de DLQ (Dead Letter Queue) para mensagens inválidas? Reprocessamento manual/automático?
- `DebitarValorSimples` usa Mongo externo fixo. Essa rotina ainda é necessária? Se sim, qual string/DB corretos e como parametrizar com segredos?
- Consolidar os contextos Mongo (estáticos) em um `IMongoClient` singleton injetado para melhor uso de conexões.
- Classe `CalcularSimples`: qual o escopo do motor fiscal (anexos, sublimites, RBT12, DIFAL, ST)? Qual será a fonte de regras (tabelas externas, parametrização própria)?
- Há necessidade de auditoria completa (quem importou, origem do XML, timestamps, correções manuais)?
