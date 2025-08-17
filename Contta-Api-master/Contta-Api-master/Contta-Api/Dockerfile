FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
ADD ./ /app
ENV ASPNETCORE_URLS=http://*:${PORT}
ENV ASPNETCORE_ENVIRONMENT=Production
WORKDIR /app
ENTRYPOINT [ "dotnet", "Contta.Inteligencia.Cnpj.dll"]