
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
//--------
using StreamHub.Models;
using StreamHub.Context;
using StreamHub.Services;
using StreamHub.Interfaces;

using Microsoft.OpenApi.Models;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;


using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using System.Text;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Config JWT y Auth se configuran más abajo

builder.Services.AddDbContext<StreamHubDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();

// Agregar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "StreamHub API",
        Version = "v1",
        Description = "API base para StreamHub"
    });
});

builder.Services.AddScoped<IUsuarioService, UsuarioDbService>();
builder.Services.AddScoped<ISuscripcionService, SuscripcionDbService>();
builder.Services.AddScoped<IContenidoService, ContenidoDbService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Bind opciones de JWT desde configuración
builder.Services.Configure<AuthOptions>(builder.Configuration.GetSection("Jwt"));

// Configurar autenticación JWT Bearer
var jwtSection = builder.Configuration.GetSection("Jwt");
var jwtKey = jwtSection.GetValue<string>("Key");
var jwtIssuer = jwtSection.GetValue<string>("Issuer");
var jwtAudience = jwtSection.GetValue<string>("Audience");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey ?? string.Empty)),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseHttpsRedirection();

// Habilitar Swagger UI (solo en Desarrollo)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "StreamHub API v1");
        c.RoutePrefix = "swagger"; // UI en /swagger
    });
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
