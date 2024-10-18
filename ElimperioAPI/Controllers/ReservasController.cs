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
            return Ok(await _reservaService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Reservas reserva)
        {
            //if (reserva == null)
            //    return BadRequest();

            //// podria tener mas validaciones
            //if (product.Name == string.Empty || product.Price < 0)
            //    ModelState.AddModelError("Error al crear producto", "Agregue un nombre valido y un precio correcto");

            await _reservaService.Create(reserva);

            return Created("Created", true);
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
    }
}
