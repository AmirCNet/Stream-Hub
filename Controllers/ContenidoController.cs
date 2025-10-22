namespace StreamHub.Controllers;
using Microsoft.AspNetCore.Mvc;
using StreamHub.Models;

[ApiController]
[Route("api/[controller]")]

public class ContenidoController : ControllerBase
{
    [HttpGet]
    public IActionResult GetContenidos()
    {
        // Aquí iría la lógica para obtener la lista de contenidos
        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult GetContenido(int id)
    {
        // Aquí iría la lógica para obtener un contenido por su ID
        return Ok();
    }

    [HttpPost]
    public IActionResult CrearContenido([FromBody] Contenido contenido)
    {
        // Aquí iría la lógica para crear un nuevo contenido
        return CreatedAtAction(nameof(GetContenido), new { id = contenido.Id }, contenido);
    }

    [HttpPut("{id}")]
    public IActionResult ActualizarContenido(int id, [FromBody] Contenido contenido)
    {
        // Aquí iría la lógica para actualizar un contenido existente
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult EliminarContenido(int id)
    {
        // Aquí iría la lógica para eliminar un contenido
        return NoContent();
    }
}