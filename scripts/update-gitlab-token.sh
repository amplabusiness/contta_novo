#!/bin/bash
# SCRIPT PARA ATUALIZAR TOKEN GITLAB AUTOMATICAMENTE

echo "🔐 ATUALIZANDO TOKEN GITLAB..."

# Pedir novo token ao usuário
echo "📋 Cole o novo token GitLab aqui:"
read -s NEW_GITLAB_TOKEN

# Validar formato do token
if [[ $NEW_GITLAB_TOKEN =~ ^glpat- ]]; then
    echo "✅ Token válido detectado!"
else
    echo "❌ Token inválido! Deve começar com 'glpat-'"
    exit 1
fi

# Atualizar no script de setup
echo "🔄 Atualizando script setup-gitlab-access.sh..."
sed -i "s/GITLAB_TOKEN=\".*\"/GITLAB_TOKEN=\"$NEW_GITLAB_TOKEN\"/" scripts/setup-gitlab-access.sh

# Testar token
echo "🧪 Testando novo token..."
RESPONSE=$(curl -s -H "Authorization: Bearer $NEW_GITLAB_TOKEN" "https://gitlab.com/api/v4/user")

if echo "$RESPONSE" | grep -q "username"; then
    echo "✅ TOKEN FUNCIONANDO!"
    USERNAME=$(echo "$RESPONSE" | jq -r '.username')
    echo "👤 Usuário: $USERNAME"
    
    # Executar setup automaticamente
    echo "🚀 Executando setup GitLab..."
    bash scripts/setup-gitlab-access.sh
    
else
    echo "❌ Token não funciona. Verifique se foi criado corretamente."
fi
