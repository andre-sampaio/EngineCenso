using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace EngineCenso.DataAccess
{
    public class Pbkdf2Hashing : IHashingAlgorithm
    {
        public string Hash(string input, string salt)
        {
            var bytes = KeyDerivation.Pbkdf2(input, Encoding.UTF8.GetBytes(salt), KeyDerivationPrf.HMACSHA512, 80000, numBytesRequested: 128);

            return Convert.ToBase64String(bytes);
        }

        public string GenerateSalt()
        {
            byte[] salt = new byte[128];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(salt);
                return Convert.ToBase64String(salt);
            }
        }
    }
}
