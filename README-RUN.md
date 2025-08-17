# Quick run: core infrastructure and NFe consumer

This guide brings up RabbitMQ + MongoDB and runs the ConsumerXml service.

## Prereqs
- Docker Desktop (Windows)
- .NET SDK 8.0 (if running locally)

## 1) Start infra
```powershell
cd g:\contta_v2
docker compose up -d
```

## 2) Run ConsumerXml (option A: local)
```powershell
$env:RABBITMQ_HOST = 'localhost'
$env:RABBITMQ_PORT = '5672'
$env:RABBITMQ_USER = 'guest'
$env:RABBITMQ_PASSWORD = 'guest'
$env:RABBITMQ_QUEUE = 'Modelo55'
$env:RABBITMQ_PREFETCH = '20'
dotnet run --project .\agendador-back-end-master\agendador-back-end-master\ConsumerXml\ConsumerXml.csproj -c Release
```

## 2) Run ConsumerXml (option B: container)
```powershell
cd g:\contta_v2\agendador-back-end-master\agendador-back-end-master\ConsumerXml
docker build -t consumerxml:latest .
# ensure infra is up
cd g:\contta_v2
docker run --rm --network host consumerxml:latest
```

RabbitMQ UI: http://localhost:15672 (guest/guest)
