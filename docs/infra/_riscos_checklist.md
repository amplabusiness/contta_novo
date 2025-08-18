# Checklist de Riscos (pré-PROD)

- [ ] Ambientes segregados (DB/AMQP distintos)
- [ ] Segredos apenas nos provedores (sem hardcode / repo)
- [ ] Healthcheck e autoscaling no Render
- [ ] JWT válido por ambiente (realm/cliente)
- [ ] CORS configurado (domínios Vercel/Render)
- [ ] DLQ + retries nas filas críticas
- [ ] Backups Atlas habilitados e testados
- [ ] Logs e métricas ativas
- [ ] Capacidade adequada (Atlas/Render/AMQP)
- [ ] Versões tagueadas; Preview Deploys ativos
