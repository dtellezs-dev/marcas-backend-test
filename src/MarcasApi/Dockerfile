# Etapa base (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

# Etapa build (compilación)
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copiamos los archivos del proyecto
COPY . .

# Restauramos dependencias
RUN dotnet restore "MarcasApi.csproj"

# Publicamos el proyecto
RUN dotnet publish "MarcasApi.csproj" -c Release -o /out

# Etapa final
FROM base AS final
WORKDIR /app
COPY --from=build /out .
ENTRYPOINT ["dotnet", "MarcasApi.dll"]
