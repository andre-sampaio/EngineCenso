using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EngineCenso.DataAccess
{
    public interface IUserProvider
    {
        Task<User> Authenticate(string userName, string plainTextPassword);
        Task CreateUser(string userName, string plainTextPassword);
    }
}
