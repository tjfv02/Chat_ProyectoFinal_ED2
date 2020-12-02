using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMongoDB.Models
{
    public class Sala
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        //participantes 
        [BsonElement("Usuario1")]
        public string UsuarioEmisor { get; set; }
        [BsonElement("Usuario2")]
        public string UsuarioReceptor { get; set; }


       // public ICollection<Mensaje> MensajesSala { get; set; }
    }
}
