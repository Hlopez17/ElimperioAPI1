using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ElimperioAPI.Models
{
    public class Compra
    {
        [BsonId][BsonRepresentation(BsonType.ObjectId)] public string? Id { get; set; }

        [BsonElement("Id Compra")]
        public int CompraId { get; set; } //Numero Factura

        [BsonElement("Username")]
        public string Username { get; set; } = string.Empty; //Se refiere al usuario/Admin que realizo la compra

        [BsonElement("Total")]
        public decimal Total { get; set; }

        [BsonElement("Proveedor")]
        public string Proveedor { get; set; } //Proveedor

        public List<Producto> Productos { get; set; } = null!; /*Es una lista donde se registran las Productos comprados*/
    }
}
