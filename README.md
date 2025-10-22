# Prueba de Conocimiento - Backend C# (MarcasAutos)

Proyecto de ejemplo que cumple con los requisitos:
- ASP.NET Core Web API (Controllers)
- Entity Framework Core con PostgreSQL (Npgsql)
- Seed data con 3 marcas de autos
- XUnit tests usando InMemory provider
- Docker Compose con servicios: postgres y api

## Estructura
- src/MarcasApi: Web API project
- tests/MarcasApi.Tests: XUnit tests
- docker-compose.yml
- README.md

## Requisitos previos
- .NET SDK 7+ instalado
- Docker & Docker Compose (si desea ejecutar con Docker)

## Pasos para ejecutar localmente
1. Abrir la carpeta `src/MarcasApi`
2. Instalar herramientas de EF Core si no las tiene:
   - `dotnet tool install --global dotnet-ef`
   - `dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL`
3. Crear la migración inicial:
   - `dotnet ef migrations add Initial -p src/MarcasApi -s src/MarcasApi`
4. Aplicar migración:
   - `dotnet ef database update -p src/MarcasApi -s src/MarcasApi`
   (asegúrese de configurar la cadena de conexión en appsettings.json o variables de entorno)
5. Ejecutar la API:
   - `dotnet run --project src/MarcasApi`

## Ejecutar con Docker Compose
1. Desde la raíz del proyecto ejecutar:
   - `docker compose up --build`
2. La API quedará disponible en http://localhost:5000 (configurable)

## Ejecutar pruebas
- `dotnet test`

--- 
Los archivos incluyen comentarios explicativos en código.
