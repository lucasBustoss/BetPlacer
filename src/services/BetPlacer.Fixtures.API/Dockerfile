# Usar a imagem base oficial do .NET SDK para compilar a aplica��o
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar os arquivos .csproj e restaurar as depend�ncias
COPY src/building_blocks/BetPlacer.Core/BetPlacer.Core.csproj ./building_blocks/BetPlacer.Core/
COPY src/services/BetPlacer.Fixtures.API/BetPlacer.Fixtures.API.csproj ./services/BetPlacer.Fixtures.API/
RUN dotnet restore ./services/BetPlacer.Fixtures.API/BetPlacer.Fixtures.API.csproj

# Copiar o restante do c�digo
COPY src/building_blocks/BetPlacer.Core/ ./building_blocks/BetPlacer.Core/
COPY src/services/BetPlacer.Fixtures.API/ ./services/BetPlacer.Fixtures.API/

# Compilar o projeto
RUN dotnet publish ./services/BetPlacer.Fixtures.API/BetPlacer.Fixtures.API.csproj -c Release -o /app/publish

# Usar uma imagem runtime do .NET para rodar a aplica��o
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Expor a porta 8080
EXPOSE 8080

# Definir o comando de entrada
ENTRYPOINT ["dotnet", "BetPlacer.Fixtures.API.dll"]