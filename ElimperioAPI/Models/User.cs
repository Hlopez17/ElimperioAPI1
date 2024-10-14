using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace ElimperioAPI.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Nombre_Completo")]
        public string Nombre_Completo { get; set; } = null!;

        [BsonElement("Username")]
        public string Username { get; set; } = string.Empty;

        [BsonElement("Password")]
        public string Contraseña { get; set; } = string.Empty;

        [BsonElement("Role")]
        public string Rol { get; set; } = null!;

        [BsonElement("Email")]
        public string Email { get; set; } = null!;

    }
}
