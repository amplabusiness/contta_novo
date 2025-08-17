#!/bin/bash
# Script para verificar status dos serviços Render em tempo real

RENDER_API_TOKEN="rnd_QYyuv9vDdFpAaG7tbBVpWsKRec0K"

echo "🔄 MONITORAMENTO EM TEMPO REAL - BLUEPRINT DEPLOY"
echo "================================================"
echo ""

# Lista todos os serviços da conta
echo "📋 Verificando serviços criados..."
curl -s -H "Authorization: Bearer $RENDER_API_TOKEN" \
  "https://api.render.com/v1/services" | \
  jq -r '.[] | select(.name | startswith("contta-")) | "\(.name): \(.id) - Status: \(.serviceDetails.publishedAt // "Not deployed yet")"'

echo ""
echo "🔄 Monitoramento contínuo iniciado..."
echo "Pressione Ctrl+C para parar"
echo ""

# Loop de monitoramento
while true; do
    echo "$(date '+%H:%M:%S') - Verificando status..."
    
    # Busca serviços que começam com "contta-"
    services=$(curl -s -H "Authorization: Bearer $RENDER_API_TOKEN" \
      "https://api.render.com/v1/services" | \
      jq -r '.[] | select(.name | startswith("contta-")) | .id')
    
    for service_id in $services; do
        service_info=$(curl -s -H "Authorization: Bearer $RENDER_API_TOKEN" \
          "https://api.render.com/v1/services/$service_id")
        
        name=$(echo "$service_info" | jq -r '.name')
        status=$(echo "$service_info" | jq -r '.serviceDetails.healthCheckStatus // "unknown"')
        
        if [ "$status" = "unknown" ]; then
            # Verificar último deploy
            deploy_status=$(curl -s -H "Authorization: Bearer $RENDER_API_TOKEN" \
              "https://api.render.com/v1/services/$service_id/deploys?limit=1" | \
              jq -r '.[0].status // "no deploys"')
            status="Deploy: $deploy_status"
        fi
        
        echo "  [$name] $status"
    done
    
    echo ""
    sleep 30
done
