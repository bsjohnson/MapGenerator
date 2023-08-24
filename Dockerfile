# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
RUN apt-get update && apt-get install -y curl
WORKDIR /buildsrc

# copy csproj and restore as distinct layers
COPY *.sln .
COPY ./src/Dungeosis.WebApp/*.csproj ./src/Dungeosis.WebApp/
COPY ./src/Dungeosis.ConsoleApp/*.csproj ./src/Dungeosis.ConsoleApp/
COPY ./test/Dungeosis.Tests/*.csproj ./test/Dungeosis.Tests/
COPY ./src/Dungeosis.ClassLib/*.csproj ./src/Dungeosis.ClassLib/
RUN dotnet restore

# copy everything else and build app
COPY ./src/Dungeosis.WebApp/. ./src/Dungeosis.WebApp/
COPY ./src/Dungeosis.ConsoleApp/. ./src/Dungeosis.ConsoleApp/
COPY ./test/Dungeosis.Tests/. ./test/Dungeosis.Tests/
COPY ./src/Dungeosis.ClassLib/. ./src/Dungeosis.ClassLib/

RUN dotnet restore ./src/Dungeosis.WebApp
RUN dotnet restore ./src/Dungeosis.ClassLib

WORKDIR /buildsrc/src/Dungeosis.WebApp
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app ./
ENV ASPNETCORE_URLS=http://0.0.0.0:5000
EXPOSE 5000
ENTRYPOINT ["dotnet", "Dungeosis.WebApp.dll", "--launch-profile", "Dungeosis.WebApp"]
