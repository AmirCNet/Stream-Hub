namespace StreamHub.Controllers;

using Microsoft.AspNetCore.Mvc;
using StreamHub.Models;

[ApiController]
[Route("api/[controller]")]
public class SuscripcionController : ControllerBase
{
    [HttpGet]
    public IActionResult GetSuscripciones()
    {
        // Aquí iría la lógica para obtener la lista de suscripciones
        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult GetSuscripcion(int id)
    {
        // Aquí iría la lógica para obtener una suscripción por su ID
        return Ok();
    }

    [HttpPost]
    public IActionResult CrearSuscripcion([FromBody] Suscripcion suscripcion)
    {
        // Aquí iría la lógica para crear una nueva suscripción
        return CreatedAtAction(nameof(GetSuscripcion), new { id = suscripcion.Id }, suscripcion);
    }

    [HttpPut("{id}")]
    public IActionResult ActualizarSuscripcion(int id, [FromBody] Suscripcion suscripcion)
    {
        // Aquí iría la lógica para actualizar una suscripción existente
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult EliminarSuscripcion(int id)
    {
        // Aquí iría la lógica para eliminar una suscripción
        return NoContent();
    }
}