using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ElimperioAPI.Models
{
    public class Caja
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }


        [BsonElement("Mesa")]
        public int NumeroMesa { get; set; }
        [BsonElement("PedidoID")]
        public string? PedidoId { get; set; } // Id del pedido más reciente para referencia
        [BsonElement("Tipo de Pago")] 
        public string Tipopago { get; set; } = null!;

        [BsonElement("Total")]
        public int Total { get; set; }
        [BsonElement("Descuento")]
        public decimal Descuento { get; set; }

        [BsonElement("Fecha")]
        public DateTime FechaCobro { get; set; } = DateTime.Now; // Fecha del cobro


 
   
    }
}

