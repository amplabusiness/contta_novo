#!/bin/bash
# Script para corrigir configurações do Render via API (alternativa)

RENDER_API_TOKEN="$1"
SERVICE_ID="$2"
ROOT_DIR="$3"

if [ -z "$RENDER_API_TOKEN" ] || [ -z "$SERVICE_ID" ] || [ -z "$ROOT_DIR" ]; then
    echo "Uso: $0 <RENDER_API_TOKEN> <SERVICE_ID> <ROOT_DIR>"
    echo ""
    echo "Exemplos:"
    echo "  $0 token srv-d2gsleidbo4c73agmvhg .docker/keycloak"
    echo "  $0 token srv-abc123 contta-search-api-main/contta-search-api-main"
    echo ""
    exit 1
fi

echo "🔧 Corrigindo configuração do serviço $SERVICE_ID"
echo "Root Directory: $ROOT_DIR"

# Atualizar configuração do serviço
curl -X PATCH \
  -H "Authorization: Bearer $RENDER_API_TOKEN" \
  -H "Content-Type: application/json" \
  "https://api.render.com/v1/services/$SERVICE_ID" \
  -d "{
    \"rootDir\": \"$ROOT_DIR\",
    \"dockerfilePath\": \"Dockerfile\"
  }" | jq .

echo ""
echo "✅ Configuração atualizada!"
echo "🚀 Execute um Manual Deploy no painel do Render"
