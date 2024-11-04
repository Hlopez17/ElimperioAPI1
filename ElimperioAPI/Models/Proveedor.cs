using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ElimperioAPI.Models
{
    public class Proveedor
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }


        [BsonElement("Razón Social")]
        public string Razon_Social { get; set; } = null!;

        [BsonElement("Representante")]
        public string Representante { get; set; }

        [BsonElement("Telefono")]
        public int Telefono { get; set; }

        [BsonElement("Dirección")]
        public string Direccion { get; set; }
    }
}
