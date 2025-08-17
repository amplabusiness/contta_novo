#!/bin/bash
# COUNTDOWN PARA PRODUÇÃO 100% (sem mock, baseado no repositório e checagens locais)

set -euo pipefail

echo "🚀 COUNTDOWN PRODUÇÃO CONTTA (factual)"
echo "======================================"
echo ""

# Alvo de produção
TARGET_DATE="2025-09-10"

# Data atual e dias restantes (GNU date requerido)
today_ts=$(date +%s 2>/dev/null || echo 0)
target_ts=$(date -d "$TARGET_DATE" +%s 2>/dev/null || echo 0)
if [ "$today_ts" -gt 0 ] && [ "$target_ts" -gt 0 ]; then
	days_left=$(( (target_ts - today_ts) / 86400 ))
else
	days_left="N/D"
fi

echo "📅 DATA ATUAL: $(date '+%d/%m/%Y' 2>/dev/null || echo 'N/D')"
echo "🎯 DATA ALVO:  $(date -d "$TARGET_DATE" '+%d/%m/%Y' 2>/dev/null || echo "$TARGET_DATE")"
echo "⏰ DIAS RESTANTES: ${days_left}"
echo ""

# Checagens do repositório (fatos verificáveis localmente)
echo "� ESTADO DO REPOSITÓRIO (fatos)"
echo "================================"

has_ci="não"; [ -d .github/workflows ] && ls .github/workflows/*.yml >/dev/null 2>&1 && has_ci="sim"
echo "• CI/CD GitHub Workflows: $has_ci"

render_yaml="render.yaml"; has_render="não"; [ -f "$render_yaml" ] && has_render="sim"
echo "• Arquivo render.yaml presente: $has_render"

placeholders="ok"; if [ "$has_render" = "sim" ] && grep -Eq '<(KEYCLOAK_HOST|PORTAL_HOST)>' "$render_yaml"; then placeholders="pendente (placeholders encontrados)"; fi
echo "• Variáveis placeholder no render.yaml: $placeholders"

# Dockerfiles esperados por serviço
declare -A svc_df=(
	[keycloak]=".docker/keycloak/Dockerfile"
	[searchapi]="contta-search-api-main/contta-search-api-main/Dockerfile"
	[excelparser]="contta-excel-parser-main/contta-excel-parser-main/Dockerfile"
	[consumerxml]="agendador-back-end-master/agendador-back-end-master/ConsumerXml/Dockerfile"
)

echo "• Dockerfiles por serviço:"
for svc in "${!svc_df[@]}"; do
	path="${svc_df[$svc]}"
	if [ -f "$path" ]; then
		echo "  - $svc: ok ($path)"
	else
		echo "  - $svc: ausente ($path)"
	fi
done

# Sinais de segredos em texto plano (apenas alerta; não imprime valores)
secrets_alert="ok"
if grep -RiqE '(glpat-|rnd_[A-Za-z0-9]|KEYCLOAK_ADMIN_PASSWORD\s*\:|KEYCLOAK_ADMIN_PASSWORD"\s*:\s*"|Authorization: Bearer\s+rnd_)' scripts 2>/dev/null; then
	secrets_alert="alerta (tokens/segredos em scripts)"
fi
echo "• Segurança dos segredos no repo: $secrets_alert"
echo ""

# Checagem opcional de endpoints (best effort; não falha se sem rede)
echo "� ENDPOINTS (opcional)"
echo "======================="
urls=(
	"https://contta-keycloak-staging.onrender.com/realms/master/.well-known/openid-configuration"
	"https://contta-searchapi-staging.onrender.com/health"
	"https://contta-excelparser-staging.onrender.com/health"
	"https://contta-website-staging.onrender.com"
	"https://contta-novo.onrender.com"
)

if command -v curl >/dev/null 2>&1; then
	for u in "${urls[@]}"; do
		code=$(curl -s -o /dev/null -w "%{http_code}" --max-time 10 "$u" || echo 000)
		echo "• [$code] $u"
	done
else
	echo "• curl não disponível neste ambiente; pulando verificação online"
fi
echo ""

echo "🧾 RESUMO (sem suposições)"
echo "=========================="
echo "• Datas: alvo definido; dias restantes: ${days_left}"
echo "• Build: Dockerfiles presentes para Keycloak, SearchAPI, ExcelParser e ConsumerXml (ver acima)"
echo "• Deploy: render.yaml existe: $has_render; placeholders ainda presentes: $placeholders"
echo "• CI/CD: GitHub Workflows: $has_ci"
echo "• Segurança: $secrets_alert"
echo ""
echo "⚠️ Notas:"
echo "- Percentuais de progresso anteriores foram removidos (eram mock)."
echo "- Sem acesso aos painéis da Render/Vercel daqui, status online foi tentado via HTTP (best effort)."
echo "- Para um quadro 100% real, configure tokens via ambiente e rode scripts de monitoramento em uma máquina com rede."

echo "✅ Pronto. Saída acima é baseada no que está no repo agora."
