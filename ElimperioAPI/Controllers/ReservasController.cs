using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ElimperioAPI.Models;
using ElimperioAPI.Services;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.Extensions.Options;

namespace ElimperioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private ReservaServices _reservaService;

        public ReservasController(ReservaServices reservaService)
        {
            _reservaService = reservaService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _reservaService.Get());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var reserva = await _reservaService.GetById(id);
            if (reserva == null)
                return NotFound("Reserva no encontrada.");
            return Ok(reserva);
        }

        [HttpPost]
        public async Task<IActionResult> CrearReserva([FromBody] Reservas reserva)
        {
            try
            {
                await _reservaService.InsertarReservaAsync(reserva);
                return Ok(new { mensaje = "Reserva creada exitosamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear la reserva: {ex.Message}");
            }
        }



        [HttpPut("{id}")]

        public async Task<IActionResult> Update([FromBody] Reservas reserv, string id)
        {
            if (reserv == null)
                return BadRequest();

            //// podria tener mas validaciones
            //if (reserv.Name == string.Empty || reserv.Price < 0)
            //    ModelState.AddModelError("Error al actualizar producto", "Agregue un nombre valido y un precio correcto");

            reserv.Id = new MongoDB.Bson.ObjectId(id);
            await _reservaService.Update(reserv);
            return Created("Updated", true);
        }

        //public async Task Update(Reservas reserv)
        //{
        //    var filter = Builders<Reservas>.Filter.Eq(r => r.Id, reserv.Id);
        //    await _reservaService.ReplaceOneAsync(filter, reserv);
        //}

    }
}
