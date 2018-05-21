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
        private readonly IEngineCensoContext context = null;

        public CensoMappingMongoRepository(IEngineCensoContext context)
        {
            this.context = context;
        }

        public async Task<CensoMapping> Get(string name)
        {
            return await context.CensoMappings.Find(x => x.Name == name).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CensoMapping>> GetAll()
        {
            return await context.CensoMappings.Find(x => true).ToListAsync();
        }

        public async Task Insert(CensoMapping item)
        {
            await context.CensoMappings.InsertOneAsync(item);
        }

        public async Task<bool> Update(string name, CensoMapping item)
        {
            var prev = await Get(name);
            item.InternalId = prev.InternalId;
            item.Name = name;

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
