using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using StreamHub.Controllers;
using StreamHub.Dtos;
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

var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET")
    ?? throw new InvalidOperationException("JWT secret not set. Check your .env file.");

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
        };
    });

builder.Services.Configure<Microsoft.AspNetCore.HttpsPolicy.HttpsRedirectionOptions>(options =>
{
    options.HttpsPort = 5001;
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "StreamHub API",
        Version = "v1",
        Description = "API base para StreamHub"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
{
    Name = "Authorization",
    Type = SecuritySchemeType.Http, 
    Scheme = "Bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "Ingrese el token JWT comenzando con 'Bearer '"
});


    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
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

app.UseAuthentication();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "StreamHub API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

var swaggerUrl = "https://localhost:5001/swagger/index.html";
Console.WriteLine($"\nSwagger UI disponible en: {swaggerUrl}\n");


app.Run();
