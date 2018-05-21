using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineCenso.DataAccess
{
    public class EngineCensoContextInitializer
    {
        private readonly IEngineCensoContext context = null;
        private readonly IHashingAlgorithm hashingAlgorithm = null;

        public EngineCensoContextInitializer(IEngineCensoContext context, IHashingAlgorithm hashingAlgorithm)
        {
            this.context = context;
            this.hashingAlgorithm = hashingAlgorithm;
        }

        public async Task Seed(bool isDevelopment)
        {
            if (!context.CensoMappings.Find(x => true).Any())
            {
                await ConfigureIndexes(context.CensoMappings);
                await context.CensoMappings.InsertManyAsync(CensoMappingSeedData);
            }

            if(!context.Users.Find(x => true).Any())
            {
                await ConfigureIndexes(context.Users);

                if(isDevelopment)
                    await context.Users.InsertOneAsync(UserSeedData);
            }
        }

        private async Task ConfigureIndexes(IMongoCollection<CensoMapping> collection)
        {
            await collection.Indexes.CreateOneAsync(Builders<CensoMapping>.IndexKeys.Ascending(x => x.Name), new CreateIndexOptions() { Unique = true });
        }
        private async Task ConfigureIndexes(IMongoCollection<User> collection)
        {
            await collection.Indexes.CreateOneAsync(Builders<User>.IndexKeys.Ascending(x => x.UserName), new CreateIndexOptions() { Unique = true });
        }

        private List<CensoMapping> CensoMappingSeedData
        {
            get
            {
                return new List<CensoMapping>()
                {
                    new CensoMapping("AC", "$.cities[*]", "name", "population", "neighborhoods[*]", "name", "population"),
                    new CensoMapping("MG", "/body/region/cities/city", "name", "population", "neighborhoods/neighborhood", "name", "population"),
                    new CensoMapping("RJ", "/corpo/cidade", "nome", "populacao", "bairros/bairro", "nome", "populacao")
                };
            }
        }

        private User UserSeedData
        {
            get
            {
                string salt = hashingAlgorithm.GenerateSalt();
                string password = hashingAlgorithm.Hash("pass", salt);

                User user = new User()
                {
                    UserName = "test",
                    Salt = salt,
                    Password = password
                };

                return user;
            }
        }
    }
}
