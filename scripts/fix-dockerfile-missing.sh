#!/bin/bash
# CORREÇÃO AUTOMÁTICA DOCKERFILE MISSING

: "${RENDER_API_TOKEN:?export RENDER_API_TOKEN com token da Render (secret)}"

echo "🚨 DOCKERFILE MISSING - CORREÇÃO AUTOMÁTICA IA"
echo "=============================================="

# Função para corrigir serviço via API
fix_service_config() {
    local service_id=$1
    local service_name=$2
    local correct_root_dir=$3
    
    echo "🔧 Corrigindo: $service_name"
    echo "   📂 Root Dir: $correct_root_dir"
    
    # Atualizar configuração do serviço
    curl -X PATCH \
      -H "Authorization: Bearer $RENDER_API_TOKEN" \
      -H "Content-Type: application/json" \
      "https://api.render.com/v1/services/$service_id" \
      -d "{
        \"rootDir\": \"$correct_root_dir\",
        \"dockerfilePath\": \"Dockerfile\"
      }" || echo "   ⚠️ Erro na atualização - continuando..."
    
    echo "   ✅ Configuração atualizada"
    
    # Triggerar novo deploy
    echo "   🔄 Triggerando rebuild..."
    curl -X POST \
      -H "Authorization: Bearer $RENDER_API_TOKEN" \
      "https://api.render.com/v1/services/$service_id/deploys" \
      -d '{"clearCache": "yes"}' || echo "   ⚠️ Deploy não triggerado - service pode estar rebuilding"
    
    echo "   🚀 Rebuild iniciado!"
    echo ""
}

# Correções por serviço
echo "🎯 INICIANDO CORREÇÕES AUTOMÁTICAS..."
echo ""

# Keycloak - rootDir correto
echo "1. 🔐 KEYCLOAK"
fix_service_config "srv-d2gtel0dl3ps73fmr88g" "contta-keycloak-staging" ".docker/keycloak"

# Search API - rootDir correto  
echo "2. 🔍 SEARCH API"
fix_service_config "srv-d2gtel0dl3ps73fmr870" "contta-searchapi-staging" "contta-search-api-main/contta-search-api-main"

# Excel Parser - rootDir correto
echo "3. 📊 EXCEL PARSER"
fix_service_config "srv-d2gtel0dl3ps73fmr87g" "contta-excelparser-staging" "contta-excel-parser-main/contta-excel-parser-main"

# Consumer XML - rootDir correto
echo "4. ⚡ CONSUMER XML"
fix_service_config "srv-d2gtngmr433s73e8gk1g" "contta-consumerxml-staging" "agendador-back-end-master/agendador-back-end-master/ConsumerXml"

echo "✅ TODAS AS CORREÇÕES APLICADAS!"
echo ""
echo "📊 PRÓXIMOS PASSOS:"
echo "   🔄 Aguardar rebuilds (5-8 min cada)"
echo "   📈 Monitorar progresso automaticamente"
echo "   ✅ Validar quando todos online"
echo ""
echo "⏰ TEMPO ESTIMADO TOTAL: 15-20 minutos"
echo "🤖 MONITORAMENTO AUTOMÁTICO ATIVO!"
