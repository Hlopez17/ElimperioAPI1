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
    public class MesasController : ControllerBase
    {
        private readonly MesaService _mesaService;
        private int _id = 1;
        
        public MesasController(MesaService mesaService)
        {
            _mesaService = mesaService;
        }
        
        //Método para crear
        [HttpPost("Create")]
        public async Task<ActionResult<Mesa>> Crear([FromBody] Mesa nuevamesa)
        {
            if (nuevamesa == null)
            {
                return BadRequest("Los datos de la mesa no son válidos.");
            }


            var mesaCreada = await _mesaService.CreateMesaAsync(nuevamesa);
            return CreatedAtAction(nameof(Crear), new { id = mesaCreada.Id }, mesaCreada);
        }


        // Agregar un pedido a una mesa por su Id
        [HttpPost("{id:length(24)}/pedido")]
        public async Task<ActionResult<Mesa>> AddPedido(string id, Pedido nuevoPedido)
        {
            var mesa = await _mesaService.GetMesaByIdAsync(id);

            if (mesa == null)
            {
                return NotFound();
            }

            // Calcular el total del pedido individual
            nuevoPedido.Importe = nuevoPedido.Cantidad * nuevoPedido.Precio;

            // Agregar el nuevo pedido a la lista de pedidos de la mesa
            mesa.Pedidos1.Add(nuevoPedido);

            // Actualizar el total acumulado de la mesawh
            mesa.Total += nuevoPedido.Importe;

            // Actualizar la mesa en la base de datos
            await _mesaService.UpdateMesaAsync(mesa.Id, mesa);

            return mesa;
        }

        //[HttpPost]
        //public async Task<ActionResult<Pedido>> CrearPedido([FromBody] Pedido pedido)
        //{
        //    var pedidoCreado = await _pedidoService.CrearPedidoAsync(pedido);
        //    return CreatedAtAction(nameof(CrearPedido), new { id = pedidoCreado.Id }, pedidoCreado);
        //}


        // Actualizar un pedido existente en una mesa por el Id de la mesa y el Id del pedido
        [HttpPut("{mesaId:length(24)}/pedido/{pedidoId:length(24)}")]
        public async Task<IActionResult> UpdatePedido(string mesaId, double pedidoId, Pedido pedidoActualizado)
        {
            var mesa = await _mesaService.GetMesaByIdAsync(mesaId);

            if (mesa == null)
            {
                return NotFound();
            }

            var pedido = mesa.Pedidos1.Find(p => p.Id == pedidoId);
            if (pedido == null)
            {
                return NotFound();
            }

            // Actualizar el pedido existente
            pedido.Cantidad = pedidoActualizado.Cantidad;
            pedido.Productop = pedidoActualizado.Productop;
            pedido.Precio = pedidoActualizado.Precio;
            pedido.Importe = pedido.Cantidad * pedido.Precio;

            // Actualizar la mesa en la base de datos
            await _mesaService.UpdateMesaAsync(mesa.Id, mesa);

            return NoContent();
        }

       


    }
}


