using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMongoDB.Models.DatabaseSettings
{
    public class RoomDatabaseSettings : IRoomDatabaseSettings
    {

        public string RoomCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IRoomDatabaseSettings
    {
        string RoomCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
