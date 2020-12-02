using ApiMongoDB.Models;
using ApiMongoDB.Models.DatabaseSettings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMongoDB.Services
{
    public class MessageService
    {
        private readonly IMongoCollection<Mensaje> _messages;

        
        public MessageService(IMessageDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _messages = database.GetCollection<Mensaje>(settings.MessageCollectionName);
        }

        public List<Mensaje> Get() =>
            _messages.Find(mensaje => true).ToList();

        public Mensaje Get(string id) =>
            _messages.Find<Mensaje>(mensaje => mensaje.Id == id).FirstOrDefault();

        public Mensaje Create(Mensaje mensaje)
        {
            _messages.InsertOne(mensaje);

            return mensaje;
        }
        //public async Task Find(string json)
        //{
        //    var dummy = await _messages.Find($"{ { _id: ObjectId("507f1f77bcf86cd799439011") } }")
        //    .SingleAsync();
        //}

        public void Update(string id, Mensaje mensajeIn) =>
            _messages.ReplaceOne(mensaje => mensaje.Id == id, mensajeIn);

        public void Remove(Mensaje mensajeIn) =>
            _messages.DeleteOne(mensaje => mensaje.Id == mensajeIn.Id);

        public void Remove(string id) =>
            _messages.DeleteOne(mensaje => mensaje.Id == id);
    }
}

