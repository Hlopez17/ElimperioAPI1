using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ElimperioAPI.Models;
using ElimperioAPI.Services;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.Extensions.Options;

namespace ElimperioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CajaController:ControllerBase
    {
        private readonly CajaService _cajaService;
        public CajaController(CajaService cajaService)
        {
            _cajaService = cajaService;
        }

        // Obtener el total de una mesa pendiente
        [HttpGet("ObtenerMesa/{numeroMesa}")]
        public async Task<ActionResult<Mesa>> ObtenerMesa(int numeroMesa)
        {
            var mesa = await _cajaService.ObtenerMesaPendienteAsync(numeroMesa);
            if (mesa == null)
            {
                return NotFound("Mesa no encontrada o ya está cancelada.");
            }
            return Ok(mesa);
        }

        [HttpGet("ObtenerMesasPendientes")]
        public async Task<ActionResult<List<Mesa>>> ObtenerMesasPendientes()
        {
            var mesasPendientes = await _cajaService.ObtenerMesasPendientesAsync();

            if (mesasPendientes == null || !mesasPendientes.Any())
            {
                return NotFound("No hay mesas pendientes.");
            }

            return Ok(mesasPendientes);
        }

        // Registrar el pago y cancelar la mesa
        [HttpPost("PayRegister")]
        public async Task<ActionResult> RegistrarPago([FromBody] Caja nuevaCaja)
        {
            var exito = await _cajaService.RegistrarPagoAsync(nuevaCaja);
            if (!exito)
            {
                return BadRequest("No se pudo registrar el pago. Verifica el número de mesa o el estado.");
            }
            return Ok("Pago registrado y mesa cancelada exitosamente.");
        }

    }
}
