using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EngineCenso.DataAccess
{
    public class MongoUserProvider : IUserProvider
    {
        private readonly IEngineCensoContext context = null;
        private readonly IHashingAlgorithm hashingAlgorithm = null;

        public MongoUserProvider(IEngineCensoContext context, IHashingAlgorithm hashingAlgorithm)
        {
            this.context = context;
            this.hashingAlgorithm = hashingAlgorithm;
        }

        public async Task<User> Authenticate(string userName, string plainTextPassword)
        {
            User user = await context.Users.Find(x => x.UserName == userName).FirstOrDefaultAsync();

            if (user == null)
                return null;
            
            string hashedPass = hashingAlgorithm.Hash(plainTextPassword, user.Salt);

            if (hashedPass == user.Password)
                return user;

            return null;
        }

        public async Task CreateUser(string userName, string plainTextPassword)
        {
            string salt = hashingAlgorithm.GenerateSalt();
            string hashedPass = hashingAlgorithm.Hash(plainTextPassword, salt);

            User user = new User()
            {
                UserName = userName,
                Password = hashedPass,
                Salt = salt
            };

            await context.Users.InsertOneAsync(user);
        }
    }
}
