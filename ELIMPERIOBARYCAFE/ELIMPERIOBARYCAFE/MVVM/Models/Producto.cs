using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELIMPERIOBARYCAFE.MVVM.Models
{
    public class Producto
    {
        public string? Id { get; set; }
        public string Descripcion { get; set; } = null!;
        public double Precio { get; set; }
        public int Stock { get; set; } 
        public string Categoria {  get; set; } = null!;
    }
}
