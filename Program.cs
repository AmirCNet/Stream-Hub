
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
//--------
using StreamHub.Controllers;
using StreamHub.Dtos;
using StreamHub.Models;
using StreamHub.Context;

using Microsoft.OpenApi.Models;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

using Microsoft.EntityFrameworkCore;
using DotNetEnv;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

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

app.UseAuthorization();

app.MapControllers();

app.Run();
