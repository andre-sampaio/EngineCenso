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
        public async Task Seed(MongoConfig config)
        {
            EngineCensoContext context = new EngineCensoContext(config);

            if (!context.CensoMappings.Find(x => true).Any())
            {
                await ConfigureIndexes(context.CensoMappings);
                await context.CensoMappings.InsertManyAsync(SeedData);
            }
        }

        private async Task ConfigureIndexes(IMongoCollection<CensoMappingModel> collection)
        {
            await collection.Indexes.CreateOneAsync(Builders<CensoMappingModel>.IndexKeys.Ascending(x => x.Name), new CreateIndexOptions() { Unique = true });
        }

        private List<CensoMappingModel> SeedData
        {
            get
            {
                return new List<CensoMappingModel>()
                {
                    new CensoMappingModel("AC", "$.cities[*]", "name", "population", "neighborhoods[*]", "name", "population"),
                    new CensoMappingModel("MG", "/body/region/cities/city", "name", "population", "neighborhoods/neighborhood", "name", "population"),
                    new CensoMappingModel("RJ", "/corpo/cidade", "nome", "populacao", "bairros/bairro", "nome", "populacao")
                };
            }
        }
    }
}
