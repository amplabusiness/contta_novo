#!/bin/bash
# AUTOMAÇÃO COMPLETA - ESPECIALISTA SÊNIOR
# Deploy 100% automatizado sem intervenção humana

: "${RENDER_API_TOKEN:?export RENDER_API_TOKEN com token da Render (secret)}"

echo "🤖 ESPECIALISTA SÊNIOR - AUTOMAÇÃO TOTAL INICIADA"
echo "================================================"

# 1. SUSPENDER SERVIÇOS PROBLEMÁTICOS
echo "🧹 Limpando serviços problemáticos..."

# Buscar e suspender serviços com problemas
PROBLEM_SERVICES=$(curl -s -H "Authorization: Bearer $RENDER_API_TOKEN" \
  "https://api.render.com/v1/services" | \
  jq -r '.[] | select(.name | contains("conta_novo") or contains("contta-novo")) | .id')

for service_id in $PROBLEM_SERVICES; do
    echo "Suspendendo serviço problemático: $service_id"
    curl -s -X POST \
      -H "Authorization: Bearer $RENDER_API_TOKEN" \
      "https://api.render.com/v1/services/$service_id/suspend" || true
done

# 2. CRIAR SERVIÇOS CORRETOS VIA API
echo "🚀 Criando serviços corretos automaticamente..."

# KEYCLOAK
echo "Criando Keycloak..."
KEYCLOAK_PAYLOAD='{
  "type": "web_service",
  "name": "contta-keycloak-production",
  "repo": "https://github.com/amplabusiness/contta_novo",
  "branch": "main",
  "rootDir": ".docker/keycloak",
  "dockerfilePath": "Dockerfile",
  "plan": "starter",
  "region": "ohio",
  "envVars": [
    {"key": "KEYCLOAK_ADMIN", "value": "admin"},
  {"key": "KEYCLOAK_ADMIN_PASSWORD", "value": "${KEYCLOAK_ADMIN_PASSWORD}"}
  ],
  "healthCheckPath": "/realms/master/.well-known/openid-configuration"
}'

KEYCLOAK_RESPONSE=$(curl -s -X POST \
  -H "Authorization: Bearer $RENDER_API_TOKEN" \
  -H "Content-Type: application/json" \
  "https://api.render.com/v1/services" \
  -d "$KEYCLOAK_PAYLOAD")

KEYCLOAK_ID=$(echo "$KEYCLOAK_RESPONSE" | jq -r '.id')
KEYCLOAK_URL=$(echo "$KEYCLOAK_RESPONSE" | jq -r '.serviceDetails.url')

echo "✅ Keycloak criado: $KEYCLOAK_ID"
echo "   URL: $KEYCLOAK_URL"

# Aguardar Keycloak ficar live
echo "⏳ Aguardando Keycloak ficar live..."
while true; do
    STATUS=$(curl -s -H "Authorization: Bearer $RENDER_API_TOKEN" \
      "https://api.render.com/v1/services/$KEYCLOAK_ID/deploys?limit=1" | \
      jq -r '.[0].status // "unknown"')
    
    echo "   Status: $STATUS"
    
    if [ "$STATUS" = "live" ]; then
        break
    elif [ "$STATUS" = "failed" ]; then
        echo "❌ Keycloak falhou - continuando..."
        break
    fi
    
    sleep 15
done

# SEARCH API
echo "Criando Search API..."
SEARCH_PAYLOAD='{
  "type": "web_service", 
  "name": "contta-searchapi-production",
  "repo": "https://github.com/amplabusiness/contta_novo",
  "branch": "main",
  "rootDir": "contta-search-api-main/contta-search-api-main",
  "dockerfilePath": "Dockerfile",
  "plan": "starter",
  "region": "ohio",
  "envVars": [
    {"key": "NODE_ENV", "value": "production"},
    {"key": "PORT", "value": "5001"},
    {"key": "MONGODB_URI", "value": "mongodb://placeholder"},
    {"key": "OIDC_ISSUER", "value": "'$KEYCLOAK_URL'/realms/contta"},
    {"key": "OIDC_AUDIENCE", "value": "contta-portal"},
    {"key": "CORS_ORIGINS", "value": "http://localhost:3000,https://contta-portal.vercel.app"}
  ],
  "healthCheckPath": "/health"
}'

SEARCH_RESPONSE=$(curl -s -X POST \
  -H "Authorization: Bearer $RENDER_API_TOKEN" \
  -H "Content-Type: application/json" \
  "https://api.render.com/v1/services" \
  -d "$SEARCH_PAYLOAD")

SEARCH_ID=$(echo "$SEARCH_RESPONSE" | jq -r '.id')
echo "✅ Search API criado: $SEARCH_ID"

# EXCEL PARSER
echo "Criando Excel Parser..."
EXCEL_PAYLOAD='{
  "type": "web_service",
  "name": "contta-excelparser-production", 
  "repo": "https://github.com/amplabusiness/contta_novo",
  "branch": "main",
  "rootDir": "contta-excel-parser-main/contta-excel-parser-main",
  "dockerfilePath": "Dockerfile",
  "plan": "starter",
  "region": "ohio",
  "envVars": [
    {"key": "NODE_ENV", "value": "production"},
    {"key": "PORT", "value": "5002"},
    {"key": "OIDC_ISSUER", "value": "'$KEYCLOAK_URL'/realms/contta"},
    {"key": "OIDC_AUDIENCE", "value": "contta-portal"},
    {"key": "PRODUCTION_URL", "value": "https://contta-portal.vercel.app"}
  ],
  "healthCheckPath": "/health"
}'

EXCEL_RESPONSE=$(curl -s -X POST \
  -H "Authorization: Bearer $RENDER_API_TOKEN" \
  -H "Content-Type: application/json" \
  "https://api.render.com/v1/services" \
  -d "$EXCEL_PAYLOAD")

EXCEL_ID=$(echo "$EXCEL_RESPONSE" | jq -r '.id')
echo "✅ Excel Parser criado: $EXCEL_ID"

# CONSUMER WORKER
echo "Criando Consumer Worker..."
CONSUMER_PAYLOAD='{
  "type": "worker",
  "name": "contta-consumer-production",
  "repo": "https://github.com/amplabusiness/contta_novo", 
  "branch": "main",
  "rootDir": "agendador-back-end-master/agendador-back-end-master/ConsumerXml",
  "dockerfilePath": "Dockerfile",
  "plan": "starter",
  "region": "ohio",
  "envVars": [
    {"key": "RABBITMQ_URL", "value": "amqp://placeholder"},
    {"key": "RABBITMQ_QUEUE", "value": "Modelo55"},
    {"key": "RABBITMQ_PREFETCH", "value": "20"},
    {"key": "RabbitMQ__Durable", "value": "true"},
    {"key": "RabbitMQ__Exclusive", "value": "false"},
    {"key": "RabbitMQ__AutoDelete", "value": "false"},
    {"key": "RabbitMQ__DeadLetterExchange", "value": "dlx.nfe"},
    {"key": "RabbitMQ__DeadLetterRoutingKey", "value": "Modelo55.dlq"}
  ]
}'

CONSUMER_RESPONSE=$(curl -s -X POST \
  -H "Authorization: Bearer $RENDER_API_TOKEN" \
  -H "Content-Type: application/json" \
  "https://api.render.com/v1/services" \
  -d "$CONSUMER_PAYLOAD")

CONSUMER_ID=$(echo "$CONSUMER_RESPONSE" | jq -r '.id')
echo "✅ Consumer Worker criado: $CONSUMER_ID"

# 3. MONITORAMENTO AUTOMÁTICO
echo "🔄 Monitorando deploys automaticamente..."

monitor_service() {
    local service_id="$1"
    local service_name="$2"
    local attempts=0
    
    while [ $attempts -lt 40 ]; do
        STATUS=$(curl -s -H "Authorization: Bearer $RENDER_API_TOKEN" \
          "https://api.render.com/v1/services/$service_id/deploys?limit=1" | \
          jq -r '.[0].status // "unknown"')
        
        echo "[$service_name] Status: $STATUS"
        
        if [ "$STATUS" = "live" ]; then
            echo "✅ [$service_name] SUCESSO!"
            return 0
        elif [ "$STATUS" = "failed" ]; then
            echo "❌ [$service_name] FALHOU!"
            return 1
        fi
        
        sleep 15
        ((attempts++))
    done
}

# Monitorar todos os serviços
monitor_service "$SEARCH_ID" "Search API" &
monitor_service "$EXCEL_ID" "Excel Parser" &
monitor_service "$CONSUMER_ID" "Consumer Worker" &

wait

echo ""
echo "🎯 AUTOMAÇÃO COMPLETA FINALIZADA!"
echo "================================="
echo "Keycloak: $KEYCLOAK_URL"
echo "Search API: https://contta-searchapi-production.onrender.com"
echo "Excel Parser: https://contta-excelparser-production.onrender.com"
echo ""
echo "Service IDs para GitHub Secrets:"
echo "RENDER_SERVICE_ID_KEYCLOAK=$KEYCLOAK_ID"
echo "RENDER_SERVICE_ID_SEARCHAPI=$SEARCH_ID"
echo "RENDER_SERVICE_ID_EXCELPARSER=$EXCEL_ID"
echo "RENDER_SERVICE_ID_CONSUMERXML=$CONSUMER_ID"
