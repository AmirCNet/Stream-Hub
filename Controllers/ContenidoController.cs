namespace StreamHub.Controllers;
using Microsoft.AspNetCore.Mvc;
using StreamHub.Models;
using StreamHub.Interfaces;
using StreamHub.Dtos;

[ApiController]
[Route("api/[controller]")]

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
}