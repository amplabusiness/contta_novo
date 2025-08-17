# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish ConsumerXml.csproj -c Release -o /app /p:UseAppHost=false

# Run
FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=build /app ./
ENV RABBITMQ_HOST=rabbitmq
ENV RABBITMQ_PORT=5672
ENV RABBITMQ_VHOST=/
ENV RABBITMQ_USER=guest
ENV RABBITMQ_PASSWORD=guest
ENV RABBITMQ_QUEUE=Modelo55
ENV RABBITMQ_PREFETCH=20
ENTRYPOINT ["dotnet", "ConsumerXml.dll"]
