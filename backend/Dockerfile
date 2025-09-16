FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY src/TemplateApp.API/TemplateApp.API.csproj src/TemplateApp.API/

RUN dotnet restore src/TemplateApp.API/TemplateApp.API.csproj

COPY src/ src/

WORKDIR /src/src/TemplateApp.API

RUN dotnet publish TemplateApp.API.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "TemplateApp.API.dll"]
