using Microsoft.AspNetCore.Mvc;
using StreamHub.Models;

namespace StreamHub.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    [HttpGet]
    public IActionResult GetUsuarios()
    {
        // Aquí iría la lógica para obtener la lista de usuarios
        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult GetUsuario(int id)
    {
        // Aquí iría la lógica para obtener un usuario por su ID
        return Ok();
    }

    [HttpPost]
    public IActionResult CrearUsuario([FromBody] Usuario usuario)
    {
        // Aquí iría la lógica para crear un nuevo usuario
        return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuario);
    }

    [HttpPut("{id}")]
    public IActionResult ActualizarUsuario(int id, [FromBody] Usuario usuario)
    {
        // Aquí iría la lógica para actualizar un usuario existente
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult EliminarUsuario(int id)
    {
        // Aquí iría la lógica para eliminar un usuario
        return NoContent();
    }
}