namespace StreamHub.Controllers;

using Microsoft.AspNetCore.Mvc;
using StreamHub.Models;
using StreamHub.Interfaces;

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
        return Ok(suscripciones);
    }

    [HttpGet("{id}")]
    public IActionResult GetSuscripcion(int id)
    {
        var suscripcion = _suscripcionService.GetById(id);
        if (suscripcion == null)
            return NotFound($"No se encontró la suscripción con ID {id}");

        return Ok(suscripcion);
    }

    [HttpPost]
    public IActionResult CrearSuscripcion([FromBody] Suscripcion suscripcion)
    {
        var nuevaSuscripcion = _suscripcionService.Add(suscripcion);
        return CreatedAtAction(nameof(GetSuscripcion), new { id = nuevaSuscripcion.Id }, nuevaSuscripcion);
    }

    [HttpPut("{id}")]
    public IActionResult ActualizarSuscripcion(int id, [FromBody] Suscripcion suscripcion)
    {
        var suscripcionExistente = _suscripcionService.GetById(id);
        if (suscripcionExistente == null)
            return NotFound($"No se encontró la suscripción con ID {id}");

        // Aquí se debería actualizar la suscripción usando el servicio correspondiente.
        // Ejemplo (si existiera): _suscripcionService.Update(id, suscripcion);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult EliminarSuscripcion(int id)
    {
        var eliminado = _suscripcionService.Delete(id);
        if (!eliminado)
            return NotFound($"No se encontró la suscripción con ID {id}");

        return NoContent();
    }
}