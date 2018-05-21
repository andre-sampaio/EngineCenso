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

        public IMongoCollection<CensoMapping> CensoMappings
        {
            get
            {
                return mongoDatabase.GetCollection<CensoMapping>("CensoMapping");
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
