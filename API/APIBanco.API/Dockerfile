FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["APIBanco.API/APIBanco.API.csproj", "APIBanco.API/"]
COPY ["APIBanco.Core/APIBanco.Core.csproj", "APIBanco.Core/"]
COPY ["APIBanco.Domain/APIBanco.Domain.csproj", "APIBanco.Domain/"]
COPY ["APIBanco.InfraEstrutura/APIBanco.InfraEstrutura.csproj", "APIBanco.InfraEstrutura/"]
COPY ["APIBanco.Middlewares/APIBanco.Middlewares.csproj", "APIBanco.Middlewares/"]
COPY ["APIBanco.Services/APIBanco.Services.csproj", "APIBanco.Services/"]
RUN dotnet restore "./APIBanco.API/APIBanco.API.csproj"
COPY . .
WORKDIR "/src/APIBanco.API"
RUN dotnet build "./APIBanco.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./APIBanco.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "APIBanco.API.dll"]
