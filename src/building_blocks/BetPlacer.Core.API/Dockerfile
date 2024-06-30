# Usar a imagem base oficial do .NET SDK para compilar a aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar os arquivos .csproj e restaurar as dependências
COPY src/building_blocks/BetPlacer.Core/BetPlacer.Core.csproj ./building_blocks/BetPlacer.Core/
COPY src/building_blocks/BetPlacer.Core.API/BetPlacer.Core.API.csproj ./building_blocks/BetPlacer.Core.API/
RUN dotnet restore ./building_blocks/BetPlacer.Core.API/BetPlacer.Core.API.csproj

# Copiar o restante do código
COPY src/building_blocks/BetPlacer.Core/ ./building_blocks/BetPlacer.Core/
COPY src/building_blocks/BetPlacer.Core.API/ ./building_blocks/BetPlacer.Core.API/

# Compilar o projeto
RUN dotnet publish ./building_blocks/BetPlacer.Core.API/BetPlacer.Core.API.csproj -c Release -o /app/publish

# Usar uma imagem runtime do .NET para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Expor a porta 8080
EXPOSE 8080

# Definir o comando de entrada
ENTRYPOINT ["dotnet", "BetPlacer.Core.API.dll"]
