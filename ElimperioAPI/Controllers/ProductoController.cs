﻿using Microsoft.AspNetCore.Authorization;
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
    public class ProductoController:ControllerBase
    {
        private readonly ProductoService _Productoservicios;

        public ProductoController(ProductoService productoServicio)=> _Productoservicios = productoServicio;

        
        [HttpGet]

        public async Task<List<Producto>> Obtener() => await _Productoservicios.ObtenerAsync();
       
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Producto>> Obtener(string id)
        {
            var estudiante = await _Productoservicios.ObtenerAsync(id);
            if (estudiante is null) return NotFound();
            return estudiante;
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Producto nuevoProducto)
        {
            await _Productoservicios.CrearAsync(nuevoProducto); 
            return CreatedAtAction(nameof(Obtener), new { id = nuevoProducto.Id }, nuevoProducto);
        }

        
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Actualizar(string id, Producto productoActualizado)
        {
            var producto = await _Productoservicios.ObtenerAsync(id);
            if (producto is null) return NotFound();
            productoActualizado.Id = producto.Id;
            await _Productoservicios.ActualizarAsync(id, productoActualizado);
            return NoContent();
        }


    }
}
