using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StreamHub.Dtos;
using StreamHub.Interfaces;

namespace StreamHub.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public ActionResult<LoginResponseDto> Login([FromBody] LoginDto dto)
    {
        var token = _authService.Login(dto);
        if (token == null)
        {
            return Unauthorized(new { message = "Credenciales inválidas" });
        }
        // Rol y NombreUsuario están embebidos en el token; devolvemos info mínima
        return Ok(new LoginResponseDto(token, "", dto.NombreUsuario));
    }
}
