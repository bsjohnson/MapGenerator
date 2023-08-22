FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
COPY . ./app

WORKDIR /app/src/Dungeosis.WebApp
run ["dotnet", "restore"]

WORKDIR /app/src/Dungeosis.WebApp
run ["dotnet", "build"]

ENV ASPNETCORE_URLS=http://*:5000
ENV DOTNET_URLS=http://*:5000

ENTRYPOINT ["dotnet", "run"]
