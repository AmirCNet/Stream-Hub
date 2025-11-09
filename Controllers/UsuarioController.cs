using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using StreamHub.Models;
using StreamHub.Interfaces;
using StreamHub.Dtos;
using StreamHub.Services;
using Microsoft.AspNetCore.Authorization;

namespace StreamHub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ITokenService _tokenService;

        public UsuarioController(IUsuarioService usuarioService, ITokenService tokenService)
        {
            _usuarioService = usuarioService;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public ActionResult<string> Login([FromBody] LoginDto loginDto)
        {
            var usuario = _usuarioService.GetAll()
                .FirstOrDefault(u => u.Email == loginDto.Email && u.Contraseña == loginDto.Contraseña);

            if (usuario == null)
                return Unauthorized("Credenciales inválidas");

            var token = _tokenService.GenerateToken(usuario);
            return Ok(new { token });
        private readonly ISuscripcionService _suscripcionService;

        public UsuarioController(IUsuarioService usuarioService, ISuscripcionService suscripcionService)
        {
            _usuarioService = usuarioService;
            _suscripcionService = suscripcionService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<List<UsuarioDto>> GetAll()
        {
            var usuarios = _usuarioService.GetAll();
            var usuariosDto = usuarios.Select(MapToUsuarioDto).ToList();
            return Ok(usuariosDto);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<UsuarioDto> GetById(int id)
        {
            var user = _usuarioService.GetById(id);
            if (user == null)
                return NotFound($"No se encontró el usuario con ID {id}");

            return Ok(MapToUsuarioDto(user));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult<UsuarioDto> Create([FromBody] UsuarioCreateDto dto)
        {
            var usuario = new Usuario
            {
                Nombre = dto.Nombre,
                Email = dto.Email,
                Contraseña = dto.Contraseña,
                Rol = dto.Rol
            };
            var created = _usuarioService.Add(usuario);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, MapToUsuarioDto(created));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<UsuarioDto> Update(int id, [FromBody] UsuarioCreateDto dto)
        {
            var model = new Usuario
            {
                Nombre = dto.Nombre,
                Email = dto.Email,
                Contraseña = dto.Contraseña
            };
            var updated = _usuarioService.Update(id, model);
            if (updated == null)
                return NotFound($"No se encontró el usuario con ID {id}");

            return Ok(MapToUsuarioDto(updated));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var deleted = _usuarioService.Delete(id);
            if (!deleted)
                return NotFound($"No se encontró el usuario con ID {id}");

            return NoContent();
        }

        // Opción B: Upsert idempotente de suscripción por usuario
        [HttpPut("{usuarioId}/suscripcion")]
        public ActionResult<SuscripcionDto> UpsertSuscripcion(int usuarioId, [FromBody] SuscripcionCreateDto dto)
        {
            var user = _usuarioService.GetById(usuarioId);
            if (user == null)
                return NotFound($"No se encontró el usuario con ID {usuarioId}");

            var data = new Suscripcion
            {
                Plan = dto.Plan,
                FechaExpiracion = dto.FechaExpiracion,
                Estado = dto.Estado
            };

            var result = _suscripcionService.UpsertForUser(usuarioId, data);
            var resp = new SuscripcionDto
            {
                Id = result.Id,
                UsuarioId = result.UsuarioId,
                Plan = result.Plan,
                FechaInicio = result.FechaInicio,
                FechaExpiracion = result.FechaExpiracion,
                Estado = result.Estado
            };
            return Ok(resp);
        }

        private static UsuarioDto MapToUsuarioDto(Usuario u) => new UsuarioDto
        {
            Id = u.Id,
            Nombre = u.Nombre ?? string.Empty,
            Email = u.Email ?? string.Empty,
            FechaRegistro = u.FechaRegistro
        };
    }
}
