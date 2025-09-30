using Microsoft.EntityFrameworkCore;
using VRSAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Configure Entity Framework with MySQL (Pomelo provider)
builder.Services.AddDbContext<VRSDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// Configure CORS for Next.js frontend and Cloudflare
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowNextJS", policy =>
    {
        policy.WithOrigins(
            "http://localhost:3000",      // Next.js dev
            "http://localhost:3002",      // Local API
            "https://alexanderthenotsobad.us",
            "https://*.alexanderthenotsobad.us",
            "https://*.pages.dev",        // Cloudflare Pages
            "https://*.cloudflare.com"
        )
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
    });
});

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "NVRS Menu API",
        Version = "1.0.0",
        Description = "API documentation for Virtual Restaurant Solutions (C# .NET 8 version)",
        Contact = new()
        {
            Name = "Alexander Gomez"
        }
    });
});

// Configure Kestrel to listen on port 3002
builder.WebHost.UseUrls("http://0.0.0.0:3002");

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "VRSAPI v1");
        c.RoutePrefix = "swagger"; // Swagger UI at http://localhost:3002/swagger
    });
}

app.UseCors("AllowNextJS");

app.UseAuthorization();

app.MapControllers();

// Log startup information
app.Logger.LogInformation("VRSAPI is starting...");
app.Logger.LogInformation("API available at: http://localhost:3002");
app.Logger.LogInformation("Swagger UI available at: http://localhost:3002/swagger");

app.Run();