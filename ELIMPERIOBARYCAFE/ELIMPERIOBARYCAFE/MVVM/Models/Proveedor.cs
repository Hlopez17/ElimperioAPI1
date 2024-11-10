using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELIMPERIOBARYCAFE.MVVM.Models
{
     public class Proveedor
    {
        public string? Id { get; set; }

        public string Razon_Social { get; set; } = null!;

        public string? Representante { get; set; }

        public int Telefono { get; set; }
        public string? Direccion { get; set; }
    }
}
