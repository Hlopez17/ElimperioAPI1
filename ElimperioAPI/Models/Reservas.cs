﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes; 

namespace ElimperioAPI.Models
{
    public class Reservas
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]  public ObjectId Id { get; set; }
       
        [BsonElement("Fecha")]
        public DateOnly Fecha { get; set; }
        [BsonElement("Hora")]
        public TimeOnly Hora { get; set; }
        [BsonElement("CantPersonas")]
        public int CantPersonas { get; set; }
        [BsonElement("Mesa")]
        public string Mesa { get; set; } = null!;

     
    }
}