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
        public string Idusuario { get; set; }//Identificador 
        public List<Pedido> Pedidos { get; set; } = new List<Pedido>();  // Lista de pedidos de la mesa
        public DateTime Fecha { get; set; } = DateTime.Now;  // Fecha del registro de la mesa
        public decimal Total { get; set; }  // Total acumulado de la mesa
    }

    public class Pedido
    {

        public string? Id { get; set; }
        public string Producto { get; set; }  // Nombre del producto
        public int Cantidad { get; set; }  // Cantidad solicitada
        public decimal Precio { get; set; }  // Precio del producto
        public decimal Total { get; set; }  // Total del pedido (Cantidad * Precio)
        public DateTime Fecha { get; set; } = DateTime.Now;  // Fecha de creación del pedido
    }
}

