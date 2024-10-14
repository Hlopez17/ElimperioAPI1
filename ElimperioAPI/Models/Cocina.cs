using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ElimperioAPI.Models
{
    public class Cocina
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Cajero")]
        public string Cajero { get; set; } = string.Empty;

        [BsonElement("OrdenID")]
        public string OrdenID { get; set; } = null!;
        public List<Producto> Pedido { get; set; } = new List<Producto>();

        


    }
}
