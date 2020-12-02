using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMongoDB.Models
{
    public class Mensaje
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("Mensaje")]
        public string Contenido { get; set; }
        public string Sala { get; set; } //Id de la sala
        public string Emisor { get; set; } // id usuario que escribe

        // public DateTime Hora { get; set; }
    }
}
