using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ElimperioAPI.Models
{
    public class Venta
    {
        [BsonId][BsonRepresentation(BsonType.ObjectId)] public string? Id { get; set; }

        [BsonElement("OrdenID")]
        public int OrdenID { get; set; } 

        [BsonElement("Username")]
        public string Username { get; set; } = string.Empty;

        [BsonElement("Total")]
        public decimal Total { get; set; }

        [BsonElement("Mesa")]
        public int Mesa { get; set; }

        public List<Producto> Productos { get; set; } = null!; /*= new List<Calificacion>();*/
    }
}
