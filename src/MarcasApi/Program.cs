using Microsoft.EntityFrameworkCore;
using MarcasApi.Data;
var builder = WebApplication.CreateBuilder(args);

// Configure DbContext with PostgreSQL provider using ConnectionStrings:DefaultConnection
// In Docker Compose this will be provided via env var: ConnectionStrings__DefaultConnection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? builder.Configuration["ConnectionStrings:DefaultConnection"]
    ?? "Host=localhost;Database=marcasdb;Username=postgres;Password=postgres";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Add controllers
builder.Services.AddControllers();

// Add minimal OpenAPI for convenience
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Ensure DB created on startup in development (in production prefer migrations)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    // IMPORTANT: In production use proper migrations. For demo, we ensure DB and seed.
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.MapControllers();

app.Run();
