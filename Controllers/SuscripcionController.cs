namespace StreamHub.Controllers;

using Microsoft.AspNetCore.Mvc;
using StreamHub.Models;
using StreamHub.Interfaces;
using StreamHub.Dtos;

[ApiController]
[Route("api/[controller]")]
public class SuscripcionController : ControllerBase
{
    private readonly ISuscripcionService _suscripcionService;

    public SuscripcionController(ISuscripcionService suscripcionService)
    {
        _suscripcionService = suscripcionService;
    }
    [HttpGet]
    public IActionResult GetSuscripciones()
    {
        var suscripciones = _suscripcionService.GetAll();
        var dtos = suscripciones.Select(MapToDto).ToList();
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public IActionResult GetSuscripcion(int id)
    {
        var suscripcion = _suscripcionService.GetById(id);
        if (suscripcion == null)
            return NotFound($"No se encontró la suscripción con ID {id}");

        return Ok(MapToDto(suscripcion));
    }

    [HttpPost]
    public IActionResult CrearSuscripcion([FromBody] SuscripcionCreateDto dto)
    {
        var model = new Suscripcion
        {
            IdUsuario = dto.IdUsuario,
            TipoSuscripcion = dto.TipoSuscripcion,
            FechaInicio = DateTime.Now,
            FechaFin = dto.FechaFin ?? DateTime.Now.AddMonths(1),
            Estado = dto.Estado ?? "Activa"
        };
        var nuevaSuscripcion = _suscripcionService.Add(model);
        return CreatedAtAction(nameof(GetSuscripcion), new { id = nuevaSuscripcion.Id }, MapToDto(nuevaSuscripcion));
    }

    [HttpPut("{id}")]
    public IActionResult ActualizarSuscripcion(int id, [FromBody] SuscripcionCreateDto dto)
    {
        var model = new Suscripcion
        {
            IdUsuario = dto.IdUsuario,
            TipoSuscripcion = dto.TipoSuscripcion,
            FechaFin = dto.FechaFin ?? DateTime.Now.AddMonths(1),
            Estado = dto.Estado
        };
        var updated = _suscripcionService.Update(id, model);
        if (updated == null)
            return NotFound($"No se encontró la suscripción con ID {id}");

        return Ok(MapToDto(updated));
    }

    [HttpDelete("{id}")]
    public IActionResult EliminarSuscripcion(int id)
    {
        var eliminado = _suscripcionService.Delete(id);
        if (!eliminado)
            return NotFound($"No se encontró la suscripción con ID {id}");

        return NoContent();
    }

    private static SuscripcionDto MapToDto(Suscripcion s) => new SuscripcionDto
    {
        Id = s.Id,
        IdUsuario = s.IdUsuario,
        TipoSuscripcion = s.TipoSuscripcion,
        FechaInicio = s.FechaInicio,
        FechaFin = s.FechaFin,
        Estado = s.Estado
    };
}