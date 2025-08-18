# Comandos Úteis (dev/CI)

Observação: comandos abaixo assumem Windows PowerShell. Adapte conforme seu ambiente.

## Cobertura de testes (dotnet + reportgenerator)

```powershell
# Executa testes com cobertura (XPlat Code Coverage)
dotnet test --collect:"XPlat Code Coverage"

# (Opcional) Gera relatório HTML se reportgenerator estiver disponível
# Instalação local (global): dotnet tool install -g dotnet-reportgenerator-globaltool
# Geração do relatório (ajuste o caminho do .cobertura conforme saída do test)
reportgenerator -reports:**/TestResults/*/coverage.cobertura.xml -targetdir:coverage-report -reporttypes:Html
```

## Encontrar integrações HTTP manuais

```powershell
# Requer ripgrep (rg). Instalação: winget install BurntSushi.ripgrep
rg -n "new HttpClient|RestClient|HttpRequest"
```

## Encontrar parsers e regras hardcoded

```powershell
rg -n "(Regex|XmlReader|XDocument)"
```
