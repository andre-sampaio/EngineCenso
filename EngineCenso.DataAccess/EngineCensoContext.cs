using MongoDB.Driver;
using System.Collections.Generic;

namespace EngineCenso.DataAccess
{
    public class EngineCensoContext : IEngineCensoContext
    {
        private readonly IMongoDatabase mongoDatabase;

        public EngineCensoContext(MongoConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            if (client != null)
                mongoDatabase = client.GetDatabase(config.Database);
        }

        public IMongoCollection<CensoMappingModel> CensoMappings
        {
            get
            {
                return mongoDatabase.GetCollection<CensoMappingModel>("CensoMappingModel");
            }
        }

        public IMongoCollection<User> Users
        {
            get
            {
                return mongoDatabase.GetCollection<User>("Users");
            }
        }
    }
}
