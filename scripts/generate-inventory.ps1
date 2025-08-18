#requires -Version 5.1
Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$repoRoot = Split-Path -Parent $PSScriptRoot
$outFile = Join-Path $repoRoot 'docs\_inventario_inicial.md'

# Ensure docs folder exists
$null = New-Item -ItemType Directory -Path (Join-Path $repoRoot 'docs') -Force

# Capture dotnet --info
Write-Host 'Collecting dotnet --info...'
$dotnetInfo = & dotnet --info 2>&1 | Out-String

# Find solutions
Write-Host 'Discovering .sln files...'
$solutions = Get-ChildItem -Path $repoRoot -Recurse -Filter '*.sln' -ErrorAction SilentlyContinue

# List packages per solution
$pkgSections = @()
foreach ($sln in $solutions) {
    Write-Host "Listing packages for: $($sln.FullName)"
    try {
        $pkg = & dotnet list "$($sln.FullName)" package --include-transitive 2>&1 | Out-String
    } catch {
        $pkg = "[Erro ao listar pacotes para $($sln.FullName)]: $($_.Exception.Message)"
    }
    $pkgSections += "### Pacotes – $($sln.FullName)`n```text`n$pkg`n```"
}

# Keyword scans (emulating ripgrep)
Write-Host 'Scanning for TODO/FIXME/HACK/BUG and fiscal keywords...'
$codeGlobs = '*.cs','*.ts','*.tsx','*.js','*.jsx'
$allFiles = Get-ChildItem -Path $repoRoot -Recurse -Include $codeGlobs -File -ErrorAction SilentlyContinue

$todoPattern = 'TODO|FIXME|HACK|BUG'
$todoMatches = $allFiles | ForEach-Object { try { Select-String -Path $_.FullName -Pattern $todoPattern -ErrorAction SilentlyContinue } catch {} } | Select-Object Path, LineNumber, Line

$fiscalPattern = 'Simples|PGDAS|Presumido|Real|SPED|NFe|CTe|EFD|Apuração|Base de Cálculo|CFOP|NCM|CSOSN|CST|RBT12'
$fiscalMatches = $allFiles | ForEach-Object { try { Select-String -Path $_.FullName -Pattern $fiscalPattern -ErrorAction SilentlyContinue } catch {} } | Select-Object Path, LineNumber, Line

# Build markdown
$md = @()
$md += '# Inventário Inicial – Contta (workspace)'
$md += ''
$md += 'Este relatório foi gerado pelo script scripts/generate-inventory.ps1 no Windows PowerShell.'
$md += ''
$md += '## Ambiente .NET (dotnet --info)'
$md += ''
$md += '```text'
$md += $dotnetInfo.TrimEnd()
$md += '```'
$md += ''
$md += '## Soluções (.sln) encontradas'
$md += ''
if ($solutions -and $solutions.Count -gt 0) {
    $solutions | ForEach-Object { $md += ('- ' + $_.FullName.Replace($repoRoot + '\\','')) }
} else {
    $md += '- (nenhuma solução encontrada)'
}
$md += ''
$md += '## Pacotes por solução (dotnet list package --include-transitive)'
$md += ''
if ($pkgSections.Count -gt 0) { $md += $pkgSections } else { $md += '_Sem soluções para listar._' }
$md += ''
$md += '## Ocorrências TODO/FIXME/HACK/BUG'
$md += ''
if ($todoMatches) {
    foreach ($m in $todoMatches) { $rel = $m.Path.Replace($repoRoot + '\\',''); $md += ("- $($rel):$($m.LineNumber): $($m.Line.Trim())") }
} else {
    $md += '- Nenhuma ocorrência encontrada.'
}
$md += ''
$md += '## Termos fiscais/técnicos'
$md += ''
if ($fiscalMatches) {
    foreach ($m in $fiscalMatches) { $rel = $m.Path.Replace($repoRoot + '\\',''); $md += ("- $($rel):$($m.LineNumber): $($m.Line.Trim())") }
} else {
    $md += '- Nenhuma ocorrência encontrada.'
}
$md += ''
$md += '—'
$md += 'Gerado automaticamente.'

# Write file (UTF8)
$utf8NoBom = New-Object System.Text.UTF8Encoding($false)
[System.IO.File]::WriteAllText($outFile, ($md -join [Environment]::NewLine), $utf8NoBom)

Write-Host ("Inventário salvo em: {0}" -f $outFile)
