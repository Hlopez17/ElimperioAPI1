using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ElimperioAPI.Models
{
    public class Venta
    {
        [BsonId][BsonRepresentation(BsonType.ObjectId)] public string? Id { get; set; }

        [BsonElement("OrdenID")]
        public int OrdenID { get; set; } //Numero Factura

        [BsonElement("Username")]
        public string Username { get; set; } = string.Empty; //Se refiere al usuario/mesero que atendía la mesa

        [BsonElement("Total")]
        public decimal Total { get; set; }

        [BsonElement("Mesa")]
        public int Mesa { get; set; } //Numero de Mesa

        public List<Producto> Pedido { get; set; } = null!; /*Es una lista donde se registran las comidas solicitadas*/
    }
}
