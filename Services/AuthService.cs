
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StreamHub.Dtos;
using StreamHub.Interfaces;
using StreamHub.Models;

namespace StreamHub.Services;

public class AuthService : IAuthService
{
    private readonly AuthOptions _options;
    private readonly IUsuarioService _usuarioService;

    public AuthService(IOptions<AuthOptions> options, IUsuarioService usuarioService)
    {
        _options = options.Value;
        _usuarioService = usuarioService;
    }

    public string CreateToken(CreateTokenDto createTokenDto)
    {
        var claims = new List<Claim>
        {
            // Id del usuario
            new(JwtRegisteredClaimNames.Sub, createTokenDto.Id.ToString()),
            // Nombre de login del usuario
            new(JwtRegisteredClaimNames.UniqueName, createTokenDto.NombreUsuario),
            // Nombre del usuario
            new(ClaimTypes.Name, createTokenDto.Nombre),
            // Rol del usuario
            new(ClaimTypes.Role, createTokenDto.Rol)
        };

        // --- Crear la CLAVE de firma ---
        // Se toma la "Key" del archivo de configuración, se pasa a bytes y se crea una SecurityKey.
        // "SymmetricSecurityKey" indica que se usará la misma clave para firmar y validar (HS256).
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));

        // --- Crear las CREDENCIALES ---
        // Definen cómo se firmará el token (algoritmo y clave usada).
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // --- Establecer tiempos de validez ---
        var expires = DateTime.UtcNow.AddMinutes(_options.ExpMinutes); // Fecha de expiración
        var notBefore = DateTime.UtcNow;

        // --- Crear el TOKEN ---
        // Se define el token con los datos necesarios.
        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: expires,
            notBefore: notBefore,
            signingCredentials: creds
        );

        // Se convierte el token a formato JWT y se devuelve como string.
        return new JwtSecurityTokenHandler().WriteToken(token);

    }

    public string? Login(LoginDto loginDto){
        // Ruta rápida para ambiente vacío: admin/admin
        var usuarios = _usuarioService.GetAll();
        if ((usuarios == null || usuarios.Count == 0) && loginDto.NombreUsuario == "admin" && loginDto.Contraseña == "admin")
        {
            var tokenAdmin = CreateToken(new CreateTokenDto("admin", 0, "Administrador", "Admin"));
            return tokenAdmin;
        }

        // Buscar por Email o Nombre como "usuario"
        var usuariosSafe = usuarios ?? new List<Usuario>();
        var user = usuariosSafe.FirstOrDefault(u =>
            string.Equals(u.Email, loginDto.NombreUsuario, StringComparison.OrdinalIgnoreCase) ||
            string.Equals(u.Nombre, loginDto.NombreUsuario, StringComparison.OrdinalIgnoreCase)
        );

        if (user != null && user.Contraseña == loginDto.Contraseña)
        {
            var nombre = string.IsNullOrWhiteSpace(user.Nombre) ? (user.Email ?? loginDto.NombreUsuario) : user.Nombre;
            return CreateToken(new CreateTokenDto(loginDto.NombreUsuario, user.Id, nombre, "User"));
        }

        return null;
    }
}