#!/bin/bash
# Blueprint Deploy Automation via Render API

RENDER_API_TOKEN="${RENDER_API_TOKEN:-$1}"
GITHUB_REPO="amplabusiness/contta_novo"
BLUEPRINT_PATH="render.yaml"

if [ -z "$RENDER_API_TOKEN" ]; then
  echo "❌ RENDER_API_TOKEN necessário!" >&2
  echo "Defina como variável de ambiente ou passe como 1º argumento." >&2
  exit 1
fi

echo "🚀 EXECUTANDO BLUEPRINT DEPLOY AUTOMATICAMENTE"
echo "============================================="
echo "Repo: $GITHUB_REPO"
echo "Blueprint: $BLUEPRINT_PATH"
echo ""

# 1. Criar Blueprint Deploy via API
echo "📋 Criando Blueprint Deploy..."
BLUEPRINT_PAYLOAD='{
  "type": "blueprint",
  "repo": "https://github.com/'$GITHUB_REPO'",
  "branch": "main",
  "blueprintPath": "'$BLUEPRINT_PATH'",
  "serviceDetails": [
    {
      "name": "contta-searchapi-staging", 
      "env": "docker",
      "plan": "starter",
      "envVars": {
        "NODE_ENV": "production",
        "PORT": "5001",
        "AUTH_DISABLED": "true",
        "CORS_ORIGINS": "'${CORS_ORIGINS:-http://localhost:3000,https://conttanovo.vercel.app}'",
        "MONGODB_URI": "'${MONGODB_URI:-}'"
      }
    },
    {
      "name": "contta-excelparser-staging",
      "env": "docker", 
      "plan": "starter",
      "envVars": {
        "NODE_ENV": "production",
        "PORT": "5002",
        "AUTH_DISABLED": "true",
        "PRODUCTION_URL": "'${PRODUCTION_URL:-https://conttanovo.vercel.app}'",
        "MONGODB_URI": "'${MONGODB_URI:-}'"
      }
    },
    {
      "name": "contta-consumerxml-staging",
      "env": "docker",
      "plan": "starter",
      "envVars": {
        "RABBITMQ_URL": "'${RABBITMQ_URL:-}'",
        "RABBITMQ_QUEUE": "Modelo55",
        "RABBITMQ_PREFETCH": "20",
        "RabbitMQ__Durable": "true",
        "RabbitMQ__Exclusive": "false", 
        "RabbitMQ__AutoDelete": "false",
        "RabbitMQ__DeadLetterExchange": "dlx.nfe",
        "RabbitMQ__DeadLetterRoutingKey": "Modelo55.dlq"
      }
    }
  ]
}'

# Executar Blueprint Deploy
DEPLOY_RESPONSE=$(curl -s -X POST \
  -H "Authorization: Bearer $RENDER_API_TOKEN" \
  -H "Content-Type: application/json" \
  "https://api.render.com/v1/blueprints" \
  -d "$BLUEPRINT_PAYLOAD")

echo "✅ Blueprint Deploy iniciado!"
echo "$DEPLOY_RESPONSE" | jq .

# 2. Extrair Service IDs
echo ""
echo "📋 Extraindo Service IDs..."
SEARCH_ID=$(echo "$DEPLOY_RESPONSE" | jq -r '.services[] | select(.name=="contta-searchapi-staging") | .id // empty')
EXCEL_ID=$(echo "$DEPLOY_RESPONSE" | jq -r '.services[] | select(.name=="contta-excelparser-staging") | .id // empty')
CONSUMER_ID=$(echo "$DEPLOY_RESPONSE" | jq -r '.services[] | select(.name=="contta-consumerxml-staging") | .id // empty')

if [ -z "$SEARCH_ID$EXCEL_ID$CONSUMER_ID" ]; then
  echo "⚠️ Não foi possível extrair IDs de serviço. Verifique o token e a resposta da API." >&2
fi

echo "Service IDs:"
echo "RENDER_SERVICE_ID_SEARCHAPI=$SEARCH_ID"  
echo "RENDER_SERVICE_ID_EXCELPARSER=$EXCEL_ID"
echo "RENDER_SERVICE_ID_CONSUMERXML=$CONSUMER_ID"

# 3. Monitorar Deploy Status
echo ""
echo "🔄 Monitorando progresso dos deploys..."

monitor_service() {
    local service_id="$1"
    local service_name="$2"
    local max_attempts=60
    local attempt=0
    
    while [ $attempt -lt $max_attempts ]; do
        status=$(curl -s -H "Authorization: Bearer $RENDER_API_TOKEN" \
          "https://api.render.com/v1/services/$service_id/deploys?limit=1" | \
          jq -r '.[0].status // "unknown"')
        
        echo "[$service_name] Status: $status"
        
        if [ "$status" = "live" ]; then
            echo "✅ [$service_name] Deploy concluído!"
            return 0
        elif [ "$status" = "failed" ]; then
            echo "❌ [$service_name] Deploy falhou!"
            return 1
        fi
        
        sleep 10
        ((attempt++))
    done
    
    echo "⏰ [$service_name] Timeout aguardando deploy"
    return 1
}

# Monitorar serviços
monitor_service "$SEARCH_ID" "Search API" &
monitor_service "$EXCEL_ID" "Excel Parser" &
monitor_service "$CONSUMER_ID" "Consumer Worker" &

# Aguardar todos os processos
wait

echo ""
echo "🎯 BLUEPRINT DEPLOY CONCLUÍDO!"
echo "URLs dos serviços:"
echo "- Search API: https://contta-searchapi-staging.onrender.com"
echo "- Excel Parser: https://contta-excelparser-staging.onrender.com"
echo ""
echo "📋 Service IDs para GitHub Secrets:"
echo "RENDER_SERVICE_ID_SEARCHAPI=$SEARCH_ID"
echo "RENDER_SERVICE_ID_EXCELPARSER=$EXCEL_ID" 
echo "RENDER_SERVICE_ID_CONSUMERXML=$CONSUMER_ID"
