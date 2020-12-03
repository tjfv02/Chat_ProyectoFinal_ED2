using ApiMongoDB.Models;
using ApiMongoDB.Models.DatabaseSettings;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMongoDB.Services
{
    public class UserService
    {
        private readonly IMongoCollection<Usuario> _users;

        public UserService(IUserDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _users = database.GetCollection<Usuario>(settings.UserCollectionName);
        }

        public List<Usuario> Get() =>
            _users.Find(usuario => true).ToList();

        public Usuario Get(string id) =>
            _users.Find<Usuario>(usuario => usuario.Id == id).FirstOrDefault();

        public Usuario Create(Usuario usuario)
        {
            _users.InsertOne(usuario);
            return usuario;
        }

        public void Update(string id, Usuario usuarioIn) =>
            _users.ReplaceOne(usuario => usuario.Id == id, usuarioIn);

        public void Remove(Usuario usuarioIn) =>
            _users.DeleteOne(usuario => usuario.Id == usuarioIn.Id);

        public void Remove(string id) =>
            _users.DeleteOne(usuario => usuario.Id == id);

        
    }
}
