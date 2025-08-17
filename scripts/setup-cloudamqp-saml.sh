# Script para configurar Client SAML no Keycloak via Admin REST API
# Uso: execute após ter o Keycloak rodando com realm 'contta'

KEYCLOAK_URL="${1:-https://contta-keycloak-staging.onrender.com}"
ADMIN_USER="${2:-admin}"
ADMIN_PASS="${3:-sua_senha_admin}"
REALM_NAME="${4:-contta}"

echo "🔐 Configurando CloudAMQP SAML Client no Keycloak"
echo "Keycloak: $KEYCLOAK_URL"
echo "Realm: $REALM_NAME"

# 1. Obter token de admin
echo "📡 Obtendo token de acesso..."
TOKEN=$(curl -s -X POST "$KEYCLOAK_URL/realms/master/protocol/openid-connect/token" \
  -H "Content-Type: application/x-www-form-urlencoded" \
  -d "username=$ADMIN_USER" \
  -d "password=$ADMIN_PASS" \
  -d "grant_type=password" \
  -d "client_id=admin-cli" | jq -r .access_token)

if [ "$TOKEN" = "null" ] || [ -z "$TOKEN" ]; then
  echo "❌ Falha ao obter token de admin"
  exit 1
fi

echo "✅ Token obtido com sucesso"

# 2. Criar client SAML para CloudAMQP
echo "🏗️  Criando client SAML CloudAMQP..."
CLIENT_PAYLOAD='{
  "clientId": "https://customer.cloudamqp.com/saml/metadata/d684497c-152f-4501-847c-cfd98c4db040",
  "name": "CloudAMQP SAML",
  "description": "SAML Client for CloudAMQP Integration",
  "protocol": "saml",
  "enabled": true,
  "attributes": {
    "saml.authnstatement": "true",
    "saml.server.signature": "true",
    "saml.server.signature.keyinfo.ext": "false",
    "saml.assertion.signature": "false",
    "saml.client.signature": "false",
    "saml.encrypt": "false",
    "saml.force.post.binding": "true",
    "saml_name_id_format": "email",
    "saml_signature_algorithm": "RSA_SHA256",
    "saml.signing.certificate": "",
    "saml.signing.private.key": ""
  },
  "redirectUris": ["https://customer.cloudamqp.com/login/saml"],
  "baseUrl": "https://customer.cloudamqp.com/login/saml",
  "masterSamlProcessingUrl": "https://customer.cloudamqp.com/login/saml"
}'

CLIENT_ID=$(curl -s -X POST "$KEYCLOAK_URL/admin/realms/$REALM_NAME/clients" \
  -H "Authorization: Bearer $TOKEN" \
  -H "Content-Type: application/json" \
  -d "$CLIENT_PAYLOAD" \
  -w "%{header_location}" -o /dev/null | grep -o '[^/]*$')

if [ -z "$CLIENT_ID" ]; then
  echo "❌ Falha ao criar client SAML"
  exit 1
fi

echo "✅ Client SAML criado: $CLIENT_ID"

# 3. Criar mappers
echo "🗺️  Criando mappers..."

# Email mapper
EMAIL_MAPPER='{
  "name": "email",
  "protocol": "saml",
  "protocolMapper": "saml-user-property-mapper",
  "config": {
    "attribute.nameformat": "Basic",
    "user.attribute": "email",
    "attribute.name": "email"
  }
}'

curl -s -X POST "$KEYCLOAK_URL/admin/realms/$REALM_NAME/clients/$CLIENT_ID/protocol-mappers/models" \
  -H "Authorization: Bearer $TOKEN" \
  -H "Content-Type: application/json" \
  -d "$EMAIL_MAPPER"

# CloudAMQP Roles mapper (admin hardcoded)
ROLES_MAPPER='{
  "name": "cloudamqp-admin-role",
  "protocol": "saml",
  "protocolMapper": "saml-hardcode-attribute-mapper",
  "config": {
    "attribute.nameformat": "Basic",
    "attribute.name": "84codes.roles",
    "attribute.value": "d684497c-152f-4501-847c-cfd98c4db040/admin"
  }
}'

curl -s -X POST "$KEYCLOAK_URL/admin/realms/$REALM_NAME/clients/$CLIENT_ID/protocol-mappers/models" \
  -H "Authorization: Bearer $TOKEN" \
  -H "Content-Type: application/json" \
  -d "$ROLES_MAPPER"

echo "✅ Mappers criados (email + cloudamqp-admin-role)"

# 4. Exibir informações importantes
echo ""
echo "🎯 Configuração Concluída!"
echo "================================"
echo "Client ID: https://customer.cloudamqp.com/saml/metadata/d684497c-152f-4501-847c-cfd98c4db040"
echo "ACS URL: https://customer.cloudamqp.com/login/saml"
echo ""
echo "📁 Metadados SAML (para upload no CloudAMQP):"
echo "$KEYCLOAK_URL/realms/$REALM_NAME/protocol/saml/descriptor"
echo ""
echo "👤 Role mapeada: admin (d684497c-152f-4501-847c-cfd98c4db040/admin)"
echo ""
echo "🔄 Próximos passos:"
echo "1. Baixe os metadados SAML da URL acima"
echo "2. Faça upload no CloudAMQP (Configuração SAML)"
echo "3. Ative SAML no CloudAMQP"
echo "4. Teste login via: https://customer.cloudamqp.com/login/saml"
