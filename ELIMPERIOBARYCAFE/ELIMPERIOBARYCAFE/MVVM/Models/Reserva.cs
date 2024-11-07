using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELIMPERIOBARYCAFE.MVVM.Models
{
     class Reserva
    {
        public string? Id { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public int CantPersonas { get; set; }
        public string Mesa { get; set; }
    }
}
