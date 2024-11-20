using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELIMPERIOBARYCAFE.MVVM.Models
{
   public class Mesa
   {
        public string? Id { get; set; }
        public int NumeroMesa { get; set; }  // Identificador de mesa
        public string? Idusuario { get; set; }//Identificador de usuario
        public List<Pedido>? Pedidos1 { get; set; } = new List<Pedido>();  // Lista de pedidos de la mesa
        public DateTime? Fecha { get; set; } = DateTime.Now;  // Fecha del registro de la mesa
        public decimal Total { get; set; }  // Total acumulado de la mesa
        public string? Estado { get; set; }
   }

    public class Pedido
    {
        public int? Id { get; set; }
        public string Productop { get; set; }  // Nombre del producto
        public int Cantidad { get; set; }  // Cantidad solicitada
        public decimal Precio { get; set; }  // Precio del producto
        public decimal Importe { get; set; }  // Total acumulado de la mesa
        public string Estado { get; set; }// Estado del pedido, "En proceso" sera por defecto.
    }
}

