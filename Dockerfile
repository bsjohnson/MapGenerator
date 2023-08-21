FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
COPY . ./app

WORKDIR /app/src/Dungeosis.WebApp
run ["dotnet", "restore"]

WORKDIR /app/src/Dungeosis.ClassLib
run ["dotnet", "restore"]

WORKDIR /app/src/Dungeosis.WebApp
run ["dotnet", "build"]

EXPOSE 5000/tcp
ENV ASPNETCORE_URLS http://*:5000

ENTRYPOINT ["dotnet", "run"]
