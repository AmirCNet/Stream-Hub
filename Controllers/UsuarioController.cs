using Microsoft.AspNetCore.Mvc;
using StreamHub.Models;
using StreamHub.Interfaces;

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
        public ActionResult<List<Usuario>> GetAll()
        {
            return Ok(_usuarioService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Usuario> GetById(int id)
        {
            var user = _usuarioService.GetById(id);
            if (user == null)
                return NotFound($"No se encontró el usuario con ID {id}");

            return Ok(user);
        }

        [HttpPost]
        public ActionResult<Usuario> Create([FromBody] Usuario usuario)
        {
            var created = _usuarioService.Add(usuario);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public ActionResult<Usuario> Update(int id, [FromBody] Usuario usuario)
        {
            var updated = _usuarioService.Update(id, usuario);
            if (updated == null)
                return NotFound($"No se encontró el usuario con ID {id}");

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var deleted = _usuarioService.Delete(id);
            if (!deleted)
                return NotFound($"No se encontró el usuario con ID {id}");

            return NoContent();
        }
    }
}
