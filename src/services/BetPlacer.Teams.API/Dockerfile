# Usar a imagem base oficial do .NET SDK para compilar a aplica��o
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar os arquivos .csproj e restaurar as depend�ncias
COPY src/building_blocks/BetPlacer.Core/BetPlacer.Core.csproj ./building_blocks/BetPlacer.Core/
COPY src/services/BetPlacer.Teams.API/BetPlacer.Teams.API.csproj ./services/BetPlacer.Teams.API/
RUN dotnet restore ./services/BetPlacer.Teams.API/BetPlacer.Teams.API.csproj

# Copiar o restante do c�digo
COPY src/building_blocks/BetPlacer.Core/ ./building_blocks/BetPlacer.Core/
COPY src/services/BetPlacer.Teams.API/ ./services/BetPlacer.Teams.API/

# Compilar o projeto
RUN dotnet publish ./services/BetPlacer.Teams.API/BetPlacer.Teams.API.csproj -c Release -o /app/publish

# Usar uma imagem runtime do .NET para rodar a aplica��o
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Expor a porta 8080
EXPOSE 8080

# Definir o comando de entrada
ENTRYPOINT ["dotnet", "BetPlacer.Teams.API.dll"]