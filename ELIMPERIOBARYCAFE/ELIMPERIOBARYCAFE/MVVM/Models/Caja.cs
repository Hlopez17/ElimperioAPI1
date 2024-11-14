using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELIMPERIOBARYCAFE.MVVM.Models
{
    public class Caja
    {
        public string? Id { get; set; }
        public DateTime Fecha { get; set; }
        public int NumeroMesa { get; set; }
        public string? TipoPago { get; set; }
        public decimal Descuento { get; set; }
        public decimal Total { get; set; }
        public string? PedidoId { get; set; } // Id del pedido más reciente para referencia
        public DateTime FechaCobro { get; set; } = DateTime.Now; // Fecha del cobro
    }
}
