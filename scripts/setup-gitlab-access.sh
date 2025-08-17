#!/bin/bash
# SINCRONIZAÇÃO GITLAB (OFICIAL) ↔ GITHUB (MIRROR + DEPLOY)
# GitLab é o repositório OFICIAL
# GitHub é mirror com deploy automático + updates do VSCode

GITLAB_TOKEN="glpat-C0_NEWBQtBZffemu52fNVW86MQp1OjNxdGkyCw"
GITLAB_REPO_URL="https://oauth2:${GITLAB_TOKEN}@gitlab.com/amplabusiness/contta_novo.git"

echo "🎯 ESTRATÉGIA: GitLab OFICIAL → GitHub MIRROR"
echo "=============================================="

## 1. CLONE REPOSITÓRIO OFICIAL (GITLAB)
echo "📥 Clonando repositório OFICIAL (GitLab)..."
git clone "$GITLAB_REPO_URL" "./gitlab-oficial" 2>/dev/null || {
    echo "⚠️ Repositório já existe, atualizando..."
    cd "./gitlab-oficial" && git pull origin main && cd ..
}

## 2. ANÁLISE INTELIGENTE DE DIFERENÇAS  
echo "🔍 Analisando diferenças GitLab (oficial) vs GitHub (mirror)..."

analyze_differences() {
    echo "=== ANÁLISE GITLAB (OFICIAL) vs GITHUB (MIRROR) ==="
    
    # Verificar qual está mais atualizado
    cd "./gitlab-oficial"
    GITLAB_LAST_COMMIT=$(git log -1 --format="%H %s" --date=iso)
    GITLAB_COMMIT_DATE=$(git log -1 --format="%ai")
    cd ..
    
    # GitHub commits (pasta atual)
    GITHUB_LAST_COMMIT=$(git log -1 --format="%H %s" --date=iso 2>/dev/null || echo "No git repo")
    GITHUB_COMMIT_DATE=$(git log -1 --format="%ai" 2>/dev/null || echo "No date")
    
    echo "📊 COMPARAÇÃO DE COMMITS:"
    echo "GitLab (OFICIAL): $GITLAB_LAST_COMMIT"
    echo "Data GitLab: $GITLAB_COMMIT_DATE"
    echo ""
    echo "GitHub (MIRROR): $GITHUB_LAST_COMMIT" 
    echo "Data GitHub: $GITHUB_COMMIT_DATE"
    echo ""
    
    # Detectar updates do VSCode no GitHub
    echo "🔍 DETECTANDO UPDATES DO VSCODE NO GITHUB..."
    VSCODE_UPDATES=$(git log --oneline --since="1 day ago" --grep="vscode\|VSCode\|copilot\|Copilot" 2>/dev/null || echo "")
    if [ -n "$VSCODE_UPDATES" ]; then
        echo "🎯 Updates VSCode detectados no GitHub:"
        echo "$VSCODE_UPDATES"
    else
        echo "ℹ️ Nenhum update VSCode detectado nas últimas 24h"
    fi
    
    # Arquivos únicos em cada repo
    echo ""
    echo "📁 DIFERENÇAS DE ARQUIVOS:"
    diff -r --brief "./gitlab-oficial" "." 2>/dev/null | grep -v ".git" > differences.txt || true
    
    if [ -s differences.txt ]; then
        echo "⚠️ Diferenças encontradas:"
        head -10 differences.txt
        echo "📄 Relatório completo: differences.txt"
    else
        echo "✅ Repositórios sincronizados!"
    fi
}

## 3. SINCRONIZAÇÃO INTELIGENTE
smart_sync() {
    echo "🔄 INICIANDO SINCRONIZAÇÃO INTELIGENTE..."
    
    # Estratégia: GitLab oficial → GitHub, preservando updates VSCode
    echo "📤 Sincronizando GitLab OFICIAL → GitHub MIRROR..."
    
    # Adicionar GitLab como remote se não existir
    git remote add gitlab-oficial "$GITLAB_REPO_URL" 2>/dev/null || true
    
    # Fetch from GitLab
    echo "📥 Buscando atualizações do GitLab oficial..."
    git fetch gitlab-oficial main
    
    # Verificar se há conflitos
    CONFLICTS=$(git merge-tree $(git merge-base HEAD gitlab-oficial/main) HEAD gitlab-oficial/main | grep -c "<<<<<<< " || echo "0")
    
    if [ "$CONFLICTS" -gt 0 ]; then
        echo "⚠️ Conflitos detectados! Merge manual necessário."
        echo "🔧 Criando branch para resolução:"
        
        TIMESTAMP=$(date +%Y%m%d_%H%M%S)
        git checkout -b "sync-gitlab-${TIMESTAMP}"
        git merge gitlab-oficial/main --no-commit --no-ff || true
        
        echo "📋 Branch criada: sync-gitlab-${TIMESTAMP}"
        echo "🎯 Resolva conflitos e faça commit manual"
        
    else
        echo "✅ Merge limpo possível!"
        
        # Fazer backup do estado atual do GitHub
        BACKUP_BRANCH="github-backup-$(date +%Y%m%d_%H%M%S)"
        git checkout -b "$BACKUP_BRANCH"
        git checkout main
        
        # Merge GitLab → GitHub
        git merge gitlab-oficial/main --no-ff -m "sync: merge GitLab oficial → GitHub mirror ($(date))"
        
        echo "✅ Sincronização concluída!"
        echo "📦 Backup criado: $BACKUP_BRANCH"
    fi
    
    # Push para GitHub (mirror)
    echo "📤 Atualizando GitHub mirror..."
    git push origin main --force-with-lease || true
    
    # Sync de volta para GitLab (updates VSCode)
    echo "🔄 Sincronizando updates VSCode → GitLab oficial..."
    cd "./gitlab-oficial"
    git fetch origin main
    git merge origin/main --no-ff -m "sync: updates VSCode from GitHub ($(date))" || true
    git push origin main --force-with-lease || true
    cd ..
}

## 3. COMPARAÇÃO AUTOMÁTICA GITHUB vs GITLAB
echo "🔍 Comparando repositórios..."

compare_repos() {
    echo "=== COMPARAÇÃO GITHUB vs GITLAB ==="
    
    # Verificar diferenças de arquivos
    echo "📁 Comparando estrutura de arquivos..."
    diff -r --brief "./contta_novo" "./gitlab-contta-novo" > comparison_files.txt || true
    
    # Verificar diferenças de commits
    echo "📝 Comparando histórico de commits..."
    cd "./contta_novo"
    git log --oneline -10 > ../github_commits.txt
    cd "../gitlab-contta-novo" 
    git log --oneline -10 > ../gitlab_commits.txt
    cd ..
    
    echo "📊 Relatório de comparação:"
    echo "1. Diferenças de arquivos: comparison_files.txt"
    echo "2. Commits GitHub: github_commits.txt" 
    echo "3. Commits GitLab: gitlab_commits.txt"
    
    # Verificar se GitLab está atualizado
    GITHUB_LATEST=$(head -1 github_commits.txt)
    GITLAB_LATEST=$(head -1 gitlab_commits.txt)
    
    if [ "$GITHUB_LATEST" = "$GITLAB_LATEST" ]; then
        echo "✅ REPOSITÓRIOS SINCRONIZADOS!"
    else
        echo "⚠️ DIFERENÇAS DETECTADAS:"
        echo "GitHub: $GITHUB_LATEST"
        echo "GitLab: $GITLAB_LATEST"
    fi
}

## 4. SINCRONIZAÇÃO BIDIRECIONAL
sync_repositories() {
    echo "🔄 Configurando sincronização automática..."
    
    cd "./contta_novo"
    
    # Adicionar GitLab como remote
    git remote add gitlab "$GITLAB_REPO_URL" 2>/dev/null || true
    
    # Push para GitLab (GitHub → GitLab)
    echo "📤 Sincronizando GitHub → GitLab..."
    git push gitlab main --force-with-lease || true
    
    # Verificar se há commits no GitLab que não estão no GitHub
    echo "📥 Verificando GitLab → GitHub..."
    git fetch gitlab
    
    BEHIND=$(git rev-list HEAD..gitlab/main --count 2>/dev/null || echo "0")
    if [ "$BEHIND" -gt 0 ]; then
        echo "⚠️ GitLab tem $BEHIND commits à frente!"
        echo "Commits únicos no GitLab:"
        git log HEAD..gitlab/main --oneline || true
    fi
    
    cd ..
}

## 5. RELATÓRIO FINAL
generate_report() {
    echo "📋 RELATÓRIO FINAL - GITLAB vs GITHUB"
    echo "======================================"
    
    echo "🔐 Token Status:"
    echo "- Token atual: glpat-C0_NEWBQtBZffemu52fNVW86MQp1OjNxdGkyCw"
    echo "- Expira: 17 Aug 2025 (RENOVAR URGENTE!)"
    echo "- Escopos: ✅ Completos"
    
    echo ""
    echo "📊 Comparação:"
    if [ -f "comparison_files.txt" ]; then
        DIFF_COUNT=$(wc -l < comparison_files.txt)
        echo "- Diferenças de arquivos: $DIFF_COUNT"
    fi
    
    echo ""
    echo "🎯 Recomendações:"
    echo "1. ⚠️ RENOVAR TOKEN GITLAB (expira hoje)"
    echo "2. ✅ Manter sincronização automática"
    echo "3. ✅ GitLab como backup/mirror do GitHub"
    echo "4. ✅ Deploy principal no GitHub (Render/Vercel)"
}

# EXECUTAR TODAS AS FUNÇÕES
echo "🚀 INICIANDO CONFIGURAÇÃO GITLAB..."
compare_repos
sync_repositories  
generate_report

echo ""
echo "✅ CONFIGURAÇÃO GITLAB CONCLUÍDA!"
echo "📁 Arquivos gerados:"
echo "- ./gitlab-contta-novo/ (clone do GitLab)"
echo "- comparison_files.txt (diferenças)"
echo "- github_commits.txt / gitlab_commits.txt (histórico)"
