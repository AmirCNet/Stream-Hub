using Microsoft.AspNetCore.Mvc;
using StreamHub.Services;
using StreamHub.Dtos;
using StreamHub.Models;

namespace StreamHub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagosController : ControllerBase
    {
        private readonly PagoService _pagoService;
        private readonly string _apiKey = "SIMULATED_API_KEY_ABC123"; // En producción, cargala desde DB o .env

        public PagosController(PagoService pagoService)
        {
            _pagoService = pagoService;
        }

        // POST /api/payments → crea un pago
        [HttpPost]
        public async Task<IActionResult> CrearPago([FromBody] PagoDto dto)
        {
            try
            {
                var pago = await _pagoService.CrearPagoAsync(dto, _apiKey);
                return Ok(pago);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPago(string id)
        {
            try
            {
                var pago = await _pagoService.ObtenerPagoPorIdAsync(id, _apiKey);
                return Ok(pago);
            }
            catch (Exception ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
        }

        // Endpoint requerido por PayLink para consultar facturas
        [HttpGet("/api/facturas/{facuraId}")]
        public IActionResult ObtenerFactura(string facturaId, [FromQuery] int businessId)
        {
            // Simulación: normalmente consultarías en tu DB
            var factura = new
            {
                FacturaId = facturaId,
                BusinessId = businessId,
                Usuario = "Juan Pérez",
                Monto = 1999.99m,
                Estado = "Pendiente",
                Concepto = "Suscripción Premium - Octubre"
            };
            return Ok(factura);
        }
    }
}