using Microsoft.AspNetCore.Mvc;
using StreamHub.Models;
using StreamHub.Interfaces;
using StreamHub.Dtos;

namespace StreamHub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public ActionResult<List<UsuarioDto>> GetAll()
        {
            var usuarios = _usuarioService.GetAll();
            var usuariosDto = usuarios.Select(MapToUsuarioDto).ToList();
            return Ok(usuariosDto);
        }

        [HttpGet("{id}")]
        public ActionResult<UsuarioDto> GetById(int id)
        {
            var user = _usuarioService.GetById(id);
            if (user == null)
                return NotFound($"No se encontró el usuario con ID {id}");

            return Ok(MapToUsuarioDto(user));
        }

        [HttpPost]
        public ActionResult<UsuarioDto> Create([FromBody] UsuarioCreateDto dto)
        {
            var usuario = new Usuario
            {
                Nombre = dto.Nombre,
                Email = dto.Email,
                Contraseña = dto.Contraseña
            };
            var created = _usuarioService.Add(usuario);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, MapToUsuarioDto(created));
        }

        [HttpPut("{id}")]
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
        public ActionResult Delete(int id)
        {
            var deleted = _usuarioService.Delete(id);
            if (!deleted)
                return NotFound($"No se encontró el usuario con ID {id}");

            return NoContent();
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
