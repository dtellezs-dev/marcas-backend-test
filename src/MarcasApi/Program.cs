using Microsoft.EntityFrameworkCore;
using MarcasApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Connection string
var connectionString = "Host=db;Database=marcasdb;Username=postgres;Password=postgres";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Marcas API", Version = "v1" });
});

var app = builder.Build();

// Database initialization
try
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    Console.WriteLine("Waiting for database...");
    await Task.Delay(5000);

    db.Database.EnsureCreated();
    Console.WriteLine("Database ready!");
}
catch (Exception ex)
{
    Console.WriteLine($"Database warning: {ex.Message}");
}

// CONFIGURACIÓN MÁS EXPLÍCITA DE SWAGGER
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Marcas API v1");
    c.RoutePrefix = "swagger"; // Asegura que esté en /swagger
    c.DisplayRequestDuration();
});

app.UseRouting();
app.MapControllers();

// Endpoints mínimos
app.MapGet("/", () => Results.Redirect("/swagger")); // Redirige a Swagger
app.MapGet("/health", () => "Healthy");
app.MapGet("/test", () => "API is working!");

Console.WriteLine("pplication started!");
Console.WriteLine("Swagger available at: http://localhost:5000/swagger");
Console.WriteLine("Home available at: http://localhost:5000/");
Console.WriteLine("Health available at: http://localhost:5000/health");

app.Run();