using ApiMongoDB.Models;
using ApiMongoDB.Models.DatabaseSettings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMongoDB.Services
{
    public class RoomService
    {
        private readonly IMongoCollection<Sala> _rooms;

        public RoomService(IRoomDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _rooms = database.GetCollection<Sala>(settings.RoomCollectionName);
        }

        public List<Sala> Get() =>
            _rooms.Find(sala => true).ToList();

        public Sala Get(string id) =>
            _rooms.Find<Sala>(sala => sala.Id == id).FirstOrDefault();

        public Sala Create(Sala sala)
        {
            _rooms.InsertOne(sala);
            return sala;
        }

        public void Update(string id, Sala salaIn) =>
            _rooms.ReplaceOne(sala => sala.Id == id, salaIn);

        public void Remove(Sala salaIn) =>
            _rooms.DeleteOne(sala => sala.Id == salaIn.Id);

        public void Remove(string id) =>
            _rooms.DeleteOne(sala => sala.Id == id);
    }
}

