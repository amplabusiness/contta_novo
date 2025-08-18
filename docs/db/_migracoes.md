# Migrações / Evolução de Esquema (MongoDB)

MongoDB não usa migrations como EF Core, mas registre aqui mudanças relevantes no esquema/coleções e scripts de ajuste.

- 2023-..: Introdução de TbDashboardClientes (Simples) com campos ValorContabil.*
- 2024-..: Ajustes em NFE (campos de totais/base)

Sugestão: manter scripts (mongosh) versionados para criação de índices e migrações de dados; exportar difs em `docs/db/export/`.
