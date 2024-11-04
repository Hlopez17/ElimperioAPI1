using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace ElimperioAPI.Models
{
    public class Producto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }


        [BsonElement("Descripcion")]
        public string Descripcion { get; set; } = null!;

        [BsonElement("Precio")]
        public decimal Precio { get; set; } 

        [BsonElement("Stock")]
        public int Stock { get; set; }

        [BsonElement("Categoría")]
        public string Categoria { get; set; }

        //public List<Proveedor> Proveedores { get; set;} = new List<Proveedor>(); 
    }
}
