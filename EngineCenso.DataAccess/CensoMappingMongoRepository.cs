using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EngineCenso.DataAccess
{
    public class CensoMappingMongoRepository : ICensoMappingRepository
    {
        private readonly EngineCensoContext context = null;

        public CensoMappingMongoRepository(MongoConfig config)
        {
            context = new EngineCensoContext(config);
        }

        public async Task<CensoMappingModel> Get(string name)
        {
            return await context.CensoMappings.Find(x => x.Name == name).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CensoMappingModel>> GetAll()
        {
            return await context.CensoMappings.Find(x => true).ToListAsync();
        }

        public async Task Insert(CensoMappingModel item)
        {
            await context.CensoMappings.InsertOneAsync(item);
        }

        public async Task<bool> Update(string name, CensoMappingModel item)
        {
            var prev = await Get(name);
            item.InternalId = prev.InternalId;

            ReplaceOneResult actionResult = await context.CensoMappings.ReplaceOneAsync(n => n.Name.Equals(name), item, new UpdateOptions { IsUpsert = true });
            return actionResult.IsAcknowledged && actionResult.ModifiedCount > 0;
        }

        public async Task<bool> Delete(string name)
        {
            DeleteResult actionResult = await context.CensoMappings.DeleteOneAsync(x => x.Name == name);

            return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
        }
    }
}
