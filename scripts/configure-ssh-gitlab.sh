#!/bin/bash
# CONFIGURAÇÃO AUTOMÁTICA SSH GITLAB

SSH_PUBLIC_KEY="ssh-rsa AAAAB3NzaC1yc2EAAAADAQABAAACAQDx/UOqWcNj1w5vWlz0Ugr4geRc7zvEMEXQuEYUroh4AOYCqjnNt51RCztyhRDeI5O8Fw8xNiNpI+uC6WVkDNzKWdiS/otjF3sLQw3Clr1MHjIJJ5PtXZKG+RSb4vyNugQHWI5ByoqJ/x33fpvO9Jb+imboRAKBl5aQUuCznlo1piDQDJ3fm1mrSj4Enax+Ry0dmjILsZtwK4LouAJ7cnru0/RkQPlm3HXapi8TNuZti/dVWDuycd0rQ+62SIEn3heLZupjgbCIMkxHz83M4XLBej/ofrQrrH/bfWJdjG3JCdwuyly4QK+hJMMzRjKn000jYv1Z9FOJ4aoRwkdGFuJcxUO3rh0DhnOdpvOIwX9D2rn+kXUZZfd91kJU5kJkbNLq3kfEDFRa43nrDAdcnAfnXL0YacL/CJ2WxhHkWX0d1+uOw00YKidfwJrw0uVkM9WvBNrNbJiol/GchUF18cJGD/FAbvFytQOXPWCEV6DWTVCkCJIMzbaE74woOLqDBcTyOlVAx02ho7fGRvCoCb1Rs35YYfBf8dirW0CRmyP4vF+17HWUEGWGEACYAPXqw3U1dMNPNEVqLyogzlwwnPgKCIujbdWxIQY8q8i6oW2pVkNEMz9vusBkokTXytN9QSlBAI4XRnDDiTzx/RdnbiBdHaGEg8xwG4HZi1rvB6+78Q== root@recovery.vps-kinghost.net"

echo "🔑 CONFIGURAÇÃO SSH GITLAB"
echo "=========================="

# Criar diretório SSH se não existir
mkdir -p ~/.ssh
chmod 700 ~/.ssh

# Salvar chave pública
echo "$SSH_PUBLIC_KEY" > ~/.ssh/gitlab_key.pub

echo "✅ Chave pública salva em ~/.ssh/gitlab_key.pub"

# Verificar se já existe chave privada
if [ -f ~/.ssh/gitlab_key ]; then
    echo "✅ Chave privada encontrada: ~/.ssh/gitlab_key"
    
    # Configurar SSH config
    cat >> ~/.ssh/config << EOF

# GitLab configuration
Host gitlab.com
    HostName gitlab.com
    User git
    IdentityFile ~/.ssh/gitlab_key
    IdentitiesOnly yes
EOF

    echo "✅ SSH config atualizado"
    
    # Testar conexão
    echo "🔍 Testando conexão SSH..."
    ssh -T git@gitlab.com
    
    if [ $? -eq 1 ]; then
        echo "✅ SSH funcionando! Clonando repositório..."
        git clone git@gitlab.com:amplabusiness/contta_novo.git gitlab-oficial
    else
        echo "❌ SSH falhou. Verificar permissões."
    fi
    
else
    echo "❌ Chave privada não encontrada!"
    echo "📋 Precisamos da chave privada correspondente"
    echo "🔍 Locais comuns:"
    echo "   - ~/.ssh/id_rsa"
    echo "   - ~/.ssh/gitlab_key"
    echo "   - ~/.ssh/id_ed25519"
    
    echo ""
    echo "🎯 ALTERNATIVAS:"
    echo "1. Fornecer chave privada SSH"
    echo "2. Gerar novo token GitLab"
    echo "3. Continuar com GitHub (funcionando)"
fi
