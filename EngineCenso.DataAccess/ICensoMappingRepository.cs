using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EngineCenso.DataAccess
{
    public interface ICensoMappingRepository
    {
        Task<IEnumerable<CensoMappingModel>> GetAll();
        Task<CensoMappingModel> Get(string name);
        Task Insert(CensoMappingModel item);
        Task<bool> Update(string name, CensoMappingModel item);
        Task<bool> Delete(string name);
    }
}
