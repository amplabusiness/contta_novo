# 🔐 Configuração SAML CloudAMQP + Keycloak

## 📋 Informações do CloudAMQP SAML

### URLs de Configuração:
- **URL de Login SAML/SSO**: `https://customer.cloudamqp.com/login/saml`
- **URL do Consumidor SAML/ACS**: `https://customer.cloudamqp.com/login/saml`
- **URL do Público/Entity ID**: `https://customer.cloudamqp.com/saml/metadata/d684497c-152f-4501-847c-cfd98c4db040`
- **Organization ID**: `d684497c-152f-4501-847c-cfd98c4db040`

## 🎯 Configuração no Keycloak

### 1. Criar Client SAML no Keycloak
1. **Acesse**: Admin Console → Clients → Create Client
2. **Client Type**: SAML
3. **Client ID**: `https://customer.cloudamqp.com/saml/metadata/d684497c-152f-4501-847c-cfd98c4db040`
4. **Name**: `CloudAMQP SAML`

### 2. Configurações do Client
```
Valid Redirect URIs: https://customer.cloudamqp.com/login/saml
Master SAML Processing URL: https://customer.cloudamqp.com/login/saml
IDP Initiated SSO URL Name: cloudamqp
```

### 3. Configurações SAML (Settings Tab)
```
Name ID Format: email
Force POST Binding: ON
Include AuthnStatement: ON
Sign Documents: ON
Sign Assertions: OFF
Client Signature Required: OFF
```

### 4. Mappers (Client Scopes → Mappers)
Criar os seguintes mappers:

#### Email Mapper
```
Name: email
Mapper Type: User Property
Property: email
SAML Attribute Name: email
SAML Attribute NameFormat: Basic
```

#### Roles Mapper (CRÍTICO)
```
Name: cloudamqp-roles
Mapper Type: Hardcoded attribute
Attribute Name: 84codes.roles
Attribute Value: d684497c-152f-4501-847c-cfd98c4db040/admin
SAML Attribute NameFormat: Basic
```

## 🏢 Roles do CloudAMQP

### Roles Disponíveis:
- `d684497c-152f-4501-847c-cfd98c4db040/admin` - Acesso total
- `d684497c-152f-4501-847c-cfd98c4db040/billing manager` - Gestão de cobrança
- `d684497c-152f-4501-847c-cfd98c4db040/compliance manager` - Compliance
- `d684497c-152f-4501-847c-cfd98c4db040/devops` - Operações
- `d684497c-152f-4501-847c-cfd98c4db040/member` - Membro padrão
- `d684497c-152f-4501-847c-cfd98c4db040/monitor` - Apenas visualização

### Recomendação para sergio@amplabusiness.com.br:
**Role**: `admin` (acesso total para configuração inicial)

## 📁 Metadados do Keycloak

### Para obter os metadados:
1. **URL**: `https://<KEYCLOAK_HOST>/realms/contta/protocol/saml/descriptor`
2. **Baixe o XML** e faça upload no CloudAMQP

### Exemplo de URL:
```
https://contta-keycloak-staging.onrender.com/realms/contta/protocol/saml/descriptor
```

## 🔧 Configuração Avançada (Opcional)

### Mapeamento Dinâmico de Roles por Grupo
Se quiser mapear grupos do Keycloak para roles do CloudAMQP:

```
Name: group-to-cloudamqp-roles
Mapper Type: Group Membership
Group: /cloudamqp-admins
SAML Attribute Name: 84codes.roles
SAML Attribute Value: d684497c-152f-4501-847c-cfd98c4db040/admin
```

## ⚡ Passos para Ativação

### 1. No Keycloak:
- ✅ Criar client SAML
- ✅ Configurar mappers (email + roles)
- ✅ Ativar client

### 2. No CloudAMQP:
- ✅ Baixar metadados do Keycloak
- ✅ Fazer upload dos metadados
- ✅ Ativar SAML
- ✅ Testar login

## 🧪 Teste da Configuração

### Fluxo de Teste:
1. **Logout** do CloudAMQP
2. **Acesse**: `https://customer.cloudamqp.com/login/saml`
3. **Redirecionamento** para Keycloak
4. **Login** com sergio@amplabusiness.com.br
5. **Redirecionamento** de volta para CloudAMQP com role admin

## 🚨 Backup de Segurança
Antes de ativar SAML, certifique-se de:
- Ter acesso alternativo (usuário local)
- Testar em ambiente de desenvolvimento primeiro
- Documentar as configurações

## 📋 Checklist Final
- [ ] Client SAML criado no Keycloak
- [ ] Mappers configurados (email + 84codes.roles)
- [ ] Metadados baixados
- [ ] Upload no CloudAMQP realizado
- [ ] SAML ativado
- [ ] Teste de login realizado
- [ ] Acesso admin confirmado
