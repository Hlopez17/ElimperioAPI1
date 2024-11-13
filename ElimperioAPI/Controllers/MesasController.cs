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

        public MesasController(MesaService mesaService)
        {
            _mesaService = mesaService;
        }

      
        // Obtener una mesa por su Id
        [HttpPost()]
        public async Task<ActionResult<Mesa>> Crear(Mesa nuevamesa)
        {
            
                // Si no existe, creamos una nueva mesa vacía
                var nuevaMesa = new Mesa();
                await _mesaService.CreateMesaAsync(nuevaMesa);
                return nuevaMesa;
            
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
            nuevoPedido.Total = nuevoPedido.Cantidad * nuevoPedido.Precio;

            // Agregar el nuevo pedido a la lista de pedidos de la mesa
            mesa.Pedidos.Add(nuevoPedido);

            // Actualizar el total acumulado de la mesa
            mesa.Total += nuevoPedido.Total;

            // Actualizar la mesa en la base de datos
            await _mesaService.UpdateMesaAsync(mesa.Id, mesa);

            return mesa;
        }

        // Actualizar un pedido existente en una mesa por el Id de la mesa y el Id del pedido
        [HttpPut("{mesaId:length(24)}/pedido/{pedidoId:length(24)}")]
        public async Task<IActionResult> UpdatePedido(string mesaId, string pedidoId, Pedido pedidoActualizado)
        {
            var mesa = await _mesaService.GetMesaByIdAsync(mesaId);

            if (mesa == null)
            {
                return NotFound();
            }

            var pedido = mesa.Pedidos.Find(p => p.Id == pedidoId);
            if (pedido == null)
            {
                return NotFound();
            }

            // Actualizar el pedido existente
            pedido.Cantidad = pedidoActualizado.Cantidad;
            pedido.Producto = pedidoActualizado.Producto;
            pedido.Precio = pedidoActualizado.Precio;
            pedido.Total = pedido.Cantidad * pedido.Precio;

            // Actualizar la mesa en la base de datos
            await _mesaService.UpdateMesaAsync(mesa.Id, mesa);

            return NoContent();
        }
    }
}


