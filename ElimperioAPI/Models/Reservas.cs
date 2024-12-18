﻿using Microsoft.VisualBasic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ElimperioAPI.Models
{
    public class Reservas
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]  
        public string? Id { get; set; }
       
        [BsonElement("Fecha")]
        public string Fecha  { get; set; }
        [BsonElement("Hora")]
        public string Hora { get; set; }
        [BsonElement("CantPersonas")]
        public int CantPersonas { get; set; }
        [BsonElement("Mesa")]
        public string Mesa { get; set; } 

     
    }
}
