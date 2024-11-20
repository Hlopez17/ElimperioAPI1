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
    public class ProductoController:ControllerBase
    {
        private readonly ProductoService _Productoservicios;

        public ProductoController(ProductoService productoServicio)=> _Productoservicios = productoServicio;

        
        [HttpGet("Get")]

        public async Task<List<Producto>> Obtener() => await _Productoservicios.ObtenerAsync();
       
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Producto>> Obtener(string id)
        {
            var estudiante = await _Productoservicios.ObtenerAsync(id);
            if (estudiante is null) return NotFound();
            return estudiante;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Crear(Producto nuevoProducto)
        {
            await _Productoservicios.CrearAsync(nuevoProducto); 
            return CreatedAtAction(nameof(Obtener), new { id = nuevoProducto.Id }, nuevoProducto);
        }

        
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Actualizar(string id, Producto productoActualizado)
        {
            var estudiante = await _Productoservicios.ObtenerAsync(id);
            if (estudiante is null) return NotFound();
            productoActualizado.Id = estudiante.Id;
            await _Productoservicios.ActualizarAsync(productoActualizado);
            return Ok("Producto Actualizado exitosamente.");
        }

        [HttpDelete("Delete{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _Productoservicios.EliminarAsync(id);
            return Ok("Producto Eliminado exitosamente.");
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<List<Producto>>> BuscarPorNombre([FromQuery] string nombre)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                return BadRequest("El nombre del producto es requerido para la búsqueda.");
            }

            var productos = await _Productoservicios.BuscarPorNombreAsync(nombre);

            if (productos == null || productos.Count == 0)
            {
                return NotFound("No se encontraron productos con ese nombre.");
            }
            return Ok(productos);
        }

    }
}
