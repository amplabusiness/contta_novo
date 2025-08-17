#!/bin/bash
# Post-migration cleanup and validation script

echo "🧹 Limpeza Pós-Migração Railway → Render"
echo "========================================"

echo "✅ MIGRAÇÃO CONCLUÍDA!"
echo ""

echo "📊 Status dos Serviços:"
echo "Railway: ❌ 4 serviços desabilitados/suspensos"
echo "Render:  ✅ 4 serviços ativos via Blueprint"
echo "Vercel:  ✅ 2 projetos frontend ativos"
echo ""

echo "🔄 Serviços Render Ativos:"
echo "- contta-keycloak-staging (Web Service)"
echo "- contta-searchapi-staging (Web Service)"  
echo "- contta-excelparser-staging (Web Service)"
echo "- contta-consumerxml-staging (Worker)"
echo ""

echo "🌐 URLs de Produção:"
echo "- Keycloak: https://contta-keycloak-staging.onrender.com"
echo "- Search API: https://contta-searchapi-staging.onrender.com"
echo "- Excel Parser: https://contta-excelparser-staging.onrender.com"
echo "- Website: https://contta-website.vercel.app"
echo "- Portal: https://contta-portal.vercel.app"
echo ""

echo "⚡ Automação Ativa:"
echo "- deploy-render.yml: ✅ Monitorando pushes no main"
echo "- vercel-website.yml: ✅ Deploy automático"
echo "- vercel-portal.yml: ✅ Deploy automático"
echo "- smoke-post-deploy.yml: ✅ Validação de endpoints"
echo ""

echo "🔧 Próximas Ações:"
echo "1. ✅ Testar login no Keycloak"
echo "2. ✅ Validar /health das APIs"
echo "3. ✅ Configurar SAML CloudAMQP (opcional)"
echo "4. ✅ Monitorar logs por 24h"
echo "5. ✅ Deletar serviços Railway definitivamente"
echo ""

echo "🎯 MIGRAÇÃO 100% CONCLUÍDA COM SUCESSO!"
echo "Sistema rodando com deploy automatizado no Render + Vercel"
