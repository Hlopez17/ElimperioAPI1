using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELIMPERIOBARYCAFE.MVVM.Models
{
    public class Cajas
    {
        public string? Id { get; set; }
        public int NumeroMesa { get; set; }
        public string TipoPago { get; set; } = null!;
        public decimal Total { get; set; }
        public decimal Descuento { get; set; }
        public DateTime FechaCobro { get; set; } = DateTime.Now; // Fecha del cobro
    }
}
