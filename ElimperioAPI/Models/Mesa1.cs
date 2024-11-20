using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace ElimperioAPI.Models
{
    public class Mesa
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public int NumeroMesa { get; set; }  // Identificador de mesa
        public string? Idusuario {  get; set; }//Identificador de usuario
        public List<Pedido>? Pedidos1 { get; set; } = new List<Pedido>();  // Lista de pedidos de la mesa
        public DateTime? Fecha { get; set; } = DateTime.Now;  // Fecha del registro de la mesa
        public decimal Total { get; set; }  // Total acumulado de la mesa
        public string? Estado { get; set; }
    }

    public class Pedido
    {
        public int? Id { get; set; }
        public string? Productop { get; set; }  // Nombre del producto
        public int Cantidad { get; set; }  // Cantidad solicitada
        public decimal Precio { get; set; }  // Precio del producto
        public decimal Importe { get; set; }  // Total acumulado de la mesa
    }
}

