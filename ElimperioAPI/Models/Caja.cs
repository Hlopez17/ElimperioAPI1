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
        public string Mesa { get; set; } = null!;

        [BsonElement("Total")]
        public int Total { get; set; }

    }
}
