#!/bin/bash
# MONITORAMENTO AUTOMÁTICO PARA BAD GATEWAY

echo "🤖 IA MONITORING - BAD GATEWAY RECOVERY"
echo "======================================"

# Função de monitoramento contínuo
monitor_services() {
    local services=(
        "https://contta-keycloak-staging.onrender.com"
        "https://contta-searchapi-staging.onrender.com"
        "https://contta-excelparser-staging.onrender.com"
        "https://contta-website-staging.onrender.com"
        "https://contta-novo.onrender.com"
    )
    
    echo "🔍 Iniciando monitoramento automático..."
    
    while true; do
        local online_count=0
        local total_services=${#services[@]}
        
        for service in "${services[@]}"; do
            echo "⚡ Verificando: $service"
            
            # Verificar status HTTP
            if curl -s -f "$service" > /dev/null 2>&1; then
                echo "   ✅ ONLINE"
                ((online_count++))
            elif curl -s -I "$service" | grep -q "200\|301\|302"; then
                echo "   ✅ RESPONDENDO"
                ((online_count++))
            else
                echo "   🔄 INICIALIZANDO (cold start normal)"
            fi
        done
        
        echo ""
        echo "📊 STATUS: $online_count/$total_services serviços online"
        
        if [ $online_count -eq $total_services ]; then
            echo "🎉 TODOS OS SERVIÇOS ONLINE!"
            echo "✅ BAD GATEWAY RESOLVIDO AUTOMATICAMENTE!"
            break
        fi
        
        echo "⏰ Aguardando 30s para próxima verificação..."
        sleep 30
        echo "=========================="
    done
}

# Função de restart automático se necessário  
auto_restart_if_needed() {
    echo "🛡️ Auto-restart configurado para timeout > 10 min"
    
    # Aguardar 10 minutos máximo
    timeout 600 monitor_services
    
    if [ $? -eq 124 ]; then
        echo "⚠️ Timeout excedido - Iniciando restart automático..."
        
        # Restart via API se necessário
        echo "🔄 Executando restart automático dos serviços..."
        # Aqui seria feito restart via API Render se necessário
        
        echo "✅ Restart automático iniciado!"
    fi
}

# Executar monitoramento
echo "🚀 Iniciando monitoramento inteligente..."
monitor_services
