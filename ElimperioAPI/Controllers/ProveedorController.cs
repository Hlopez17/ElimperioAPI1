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
    [Authorize]
    public class ProveedorController: ControllerBase
    {
        private readonly ProveedorService _Suppliersservice;
        public ProveedorController(ProveedorService suppliersService)
        {
            _Suppliersservice = suppliersService;
        }

        [HttpGet("Get")]

        public async Task<List<Proveedor>> Obtener() => await _Suppliersservice.ObtenerAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Proveedor>> Obtener(string id)
        {
            var estudiante = await _Suppliersservice.ObtenerAsync(id);
            if (estudiante is null) return NotFound();
            return estudiante;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Crear(Proveedor newSupplier)
        {
            await _Suppliersservice.InsertarProvAsync(newSupplier);
            return CreatedAtAction(nameof(Obtener), new { id = newSupplier.Id }, newSupplier);
        }


        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Actualizar(string id, Proveedor suplierUpdate)
        {
            var estudiante = await _Suppliersservice.ObtenerAsync(id);
            if (estudiante is null) return NotFound();
            suplierUpdate.Id = estudiante.Id;
            await _Suppliersservice.ActualizarAsync(suplierUpdate);
            return Ok("Producto Actualizado exitosamente.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _Suppliersservice.EliminarAsync(id);
            return Ok("Producto Eliminado exitosamente.");
        }
    }
}
