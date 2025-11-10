using Microsoft.AspNetCore.Mvc;
using StreamHub.Services;
using StreamHub.Dtos;

namespace StreamHub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusinessController : ControllerBase
    {
        private readonly PagoService _pagoService;

        public BusinessController(PagoService pagoService)
        {
            _pagoService = pagoService;
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarNegocio([FromBody] NegocioDto dto)
        {
            try
            {
                var negocio = await _pagoService.RegistrarNegocioAsync(dto);
                return Ok(negocio);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}