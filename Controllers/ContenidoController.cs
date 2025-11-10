namespace StreamHub.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using StreamHub.Models;
using StreamHub.Interfaces;
using StreamHub.Dtos;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
[Authorize]  

public class ContenidoController : ControllerBase
{
    private readonly IContenidoService _contenidoService;

    public ContenidoController(IContenidoService contenidoService)
    {
        _contenidoService = contenidoService;
    }

    [HttpGet]
    public IActionResult GetContenidos()
    {
        var contenidos = _contenidoService.GetAll();
        var dtos = contenidos.Select(MapToDto).ToList();
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public IActionResult GetContenido(int id)
    {
        var contenido = _contenidoService.GetById(id);
        if (contenido == null) return NotFound($"No se encontró el contenido con ID {id}");
        return Ok(MapToDto(contenido));
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]  
    public IActionResult CrearContenido([FromBody] ContenidoDto dto)
    {
        var model = new Contenido
        {
            Titulo = dto.Titulo,
            Descripcion = dto.Descripcion,
            Tipo = dto.Tipo,
            Genero = dto.Genero,
            ClasificacionEdad = dto.ClasificacionEdad,
            Url = dto.Url
        };
        var creado = _contenidoService.Add(model);
        return CreatedAtAction(nameof(GetContenido), new { id = creado.Id }, MapToDto(creado));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")] 
    public IActionResult ActualizarContenido(int id, [FromBody] ContenidoDto dto)
    {
        var model = new Contenido
        {
            Titulo = dto.Titulo,
            Descripcion = dto.Descripcion,
            Tipo = dto.Tipo,
            Genero = dto.Genero,
            ClasificacionEdad = dto.ClasificacionEdad,
            Url = dto.Url
        };
        var updated = _contenidoService.Update(id, model);
        if (updated == null) return NotFound($"No se encontró el contenido con ID {id}");
        return Ok(MapToDto(updated));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]  
    public IActionResult EliminarContenido(int id)
    {
        var eliminado = _contenidoService.Delete(id);
        if (!eliminado) return NotFound($"No se encontró el contenido con ID {id}");
        return NoContent();
    }

    private static ContenidoDto MapToDto(Contenido c) => new ContenidoDto
    {
        Id = c.Id,
        Titulo = c.Titulo ?? string.Empty,
        Descripcion = c.Descripcion ?? string.Empty,
        Tipo = c.Tipo ?? string.Empty,
        Genero = c.Genero ?? string.Empty,
        ClasificacionEdad = c.ClasificacionEdad ?? string.Empty,
        Url = c.Url ?? string.Empty
    };

    [HttpGet("{id}/play")]
    public IActionResult ReproducirContenido(int id, [FromServices] ISuscripcionService _suscripcionService)
    {
        var contenido = _contenidoService.GetById(id);
        if (contenido == null)
            return NotFound(new { mensaje = $"No se encontró el contenido con ID {id}" });

        var usuarioIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if (usuarioIdClaim == null)
            return Unauthorized(new { mensaje = "No se pudo identificar al usuario." });

        int usuarioId = int.Parse(usuarioIdClaim.Value);

        var suscripcion = _suscripcionService.GetByUsuarioId(usuarioId);
        if (suscripcion == null)
            return Forbid("El usuario no tiene una suscripción activa.");

        if (suscripcion.Estado != "Activo")
            return StatusCode(403, new { mensaje = $"Acceso denegado. Estado: {suscripcion.Estado}" });

        return Ok(new
        {
            mensaje = "Acceso concedido.",
            titulo = contenido.Titulo,
            url = contenido.Url
        });
    }
}