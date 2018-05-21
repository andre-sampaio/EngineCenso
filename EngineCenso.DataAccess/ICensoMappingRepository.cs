using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EngineCenso.DataAccess
{
    public interface ICensoMappingRepository
    {
        Task<IEnumerable<CensoMapping>> GetAll();
        Task<CensoMapping> Get(string name);
        Task Insert(CensoMapping item);
        Task<bool> Update(string name, CensoMapping item);
        Task<bool> Delete(string name);
    }
}
