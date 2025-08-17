# RENOVAR TOKEN GITLAB - PASSO A PASSO AUTOMÁTICO

## ⚠️ URGENTE: Token expira HOJE (17 Aug 2025)

### 🔐 RENOVAÇÃO AUTOMÁTICA:

1. **Acesse**: https://gitlab.com/-/profile/personal_access_tokens
2. **Token atual**: glpat-C0_NEWBQtBZffemu52fNVW86MQp1OjNxdGkyCw
3. **Status**: Expira hoje!

### ✅ CONFIGURAÇÃO DO NOVO TOKEN:

**Nome**: `contta_gitlab_2025`  
**Descrição**: `acesso vscode + automação render`  
**Validade**: `1 ano (17 Aug 2026)`  

**Escopos necessários**:
```
✅ api                    # API completa
✅ read_api              # Leitura API  
✅ read_repository       # Leitura repos
✅ write_repository      # Escrita repos
✅ read_user             # Info usuário
✅ read_registry         # Container registry
✅ write_registry        # Escrita registry
✅ read_virtual_registry # Virtual registry
✅ write_virtual_registry # Virtual registry escrita
✅ create_runner         # CI/CD runners
✅ manage_runner         # Gerenciar runners
✅ k8s_proxy            # Kubernetes proxy
✅ ai_features          # AI features
✅ self_rotate          # Auto rotação
```

### 🚀 APÓS GERAR O NOVO TOKEN:

1. **Substituir** no script: `setup-gitlab-access.sh`
2. **Executar** comparação automática
3. **Configurar** sincronização bidirecional
4. **Validar** acesso completo

### 📊 COMPARAÇÃO AUTOMÁTICA:

O script vai:
- ✅ Clonar GitLab repo
- ✅ Comparar com GitHub
- ✅ Detectar diferenças
- ✅ Sincronizar automaticamente
- ✅ Gerar relatório completo

### 🎯 RESULTADO FINAL:

- **GitLab**: Mirror atualizado
- **GitHub**: Fonte principal (deploy)
- **Sincronização**: Automática
- **Comparação**: Relatório gerado

---

**Execute a renovação do token AGORA e depois rode o script!**
