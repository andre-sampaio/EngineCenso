using System;
using System.Collections.Generic;
using System.Text;

namespace EngineCenso.DataAccess
{
    public interface IHashingAlgorithm
    {
        string Hash(string input, string salt);
        string GenerateSalt();
    }
}
