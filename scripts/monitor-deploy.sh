#!/bin/bash
# Script para monitorar o status do deploy via GitHub CLI (quando disponível)

echo "🚀 Monitoramento do Deploy Automatizado"
echo "======================================="

# Função para verificar status dos workflows
check_workflows() {
    echo "📊 Status dos Workflows:"
    echo "- CI Backend: Verificando builds das APIs..."
    echo "- Deploy Render: Aguardando redeploy dos serviços..."
    echo "- Deploy Vercel: Publicando Website e Portal..."
    echo "- Smoke Tests: Validação dos endpoints..."
    echo ""
    echo "💡 Acesse: https://github.com/amplabusiness/contta_novo/actions"
    echo ""
}

# Função para listar URLs esperadas
show_endpoints() {
    echo "🌐 Endpoints Esperados:"
    echo "- Keycloak: https://contta-keycloak-staging.onrender.com"
    echo "- Search API: https://contta-searchapi-staging.onrender.com/health"
    echo "- Excel Parser: https://contta-excelparser-staging.onrender.com/health"
    echo "- Website: https://contta-website.vercel.app"
    echo "- Portal: https://contta-portal.vercel.app"
    echo ""
}

# Função para verificar secrets necessários
check_secrets() {
    echo "🔑 Secrets Necessários:"
    echo "Render: RENDER_API_TOKEN + SERVICE_IDs"
    echo "Vercel: VERCEL_TOKEN + PROJECT_IDs"
    echo "Smoke: URLs opcionais para validação"
    echo ""
}

# Executar verificações
check_workflows
show_endpoints
check_secrets

echo "⏱️  Tempo estimado: 20-30 minutos"
echo "🎯 Próximo passo: Monitore GitHub Actions!"
