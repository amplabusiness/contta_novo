# ConsumerXml (NFe Modelo 55)

Consumidor RabbitMQ para mensagens de NFe (nfeProc). Suporta payloads em XML (preferencial) e JSON.

## Configuração

Via appsettings.json (opcional) e/ou variáveis de ambiente:

```
RabbitMQ:Host                 (padrão: contta.com.br)
RabbitMQ:Port                 (padrão: 5672)
RabbitMQ:VirtualHost          (padrão: /)
RabbitMQ:User                 (padrão: guest)
RabbitMQ:Password             (padrão: guest)
RabbitMQ:Queue                (padrão: Modelo55)
RabbitMQ:Prefetch             (padrão: 20)
RabbitMQ:Durable              (padrão: false)
RabbitMQ:Exclusive            (padrão: false)
RabbitMQ:AutoDelete           (padrão: false)
RabbitMQ:DeadLetterExchange   (opcional)
RabbitMQ:DeadLetterRoutingKey (opcional)
```

Variáveis de ambiente equivalentes (sobrepõem): `RABBITMQ_HOST`, `RABBITMQ_PORT`, `RABBITMQ_VHOST`, `RABBITMQ_USER`, `RABBITMQ_PASSWORD`, `RABBITMQ_QUEUE`, `RABBITMQ_PREFETCH`.

## Execução (PowerShell)

```
dotnet build .\ConsumerXml\ConsumerXml.csproj -c Release
dotnet run --project .\ConsumerXml\ConsumerXml.csproj -c Release
```

Pressione Ctrl+C para encerrar gentilmente.

## Observabilidade

Logs estruturados (Serilog) no console com resumo da NFe (chave, emitente, destinatário, valor, itens, emissão).

## Resiliência

- QoS com prefetch (configurável)
- Ack manual somente após sucesso
- Nack sem requeue em falha (recomenda-se configurar DLQ no broker)

## Testes

Projeto `ConsumerXml.Tests` com testes de desserialização XML/JSON. Execute:

```
dotnet test .\ConsumerXml.Tests\ConsumerXml.Tests.csproj -c Release
```
