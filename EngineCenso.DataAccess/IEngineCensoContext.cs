using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace EngineCenso.DataAccess
{
    public interface IEngineCensoContext
    {
        IMongoCollection<CensoMapping> CensoMappings { get; }
        IMongoCollection<User> Users { get; }
    }
}
