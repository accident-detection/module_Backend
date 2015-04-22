using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserLibrary;

namespace UserRepo
{
    public class UserMongoDBRepo : IUserRepo
    {
        private MongoClient _client;
        private MongoUrl _connectionStirng;
        private IMongoDatabase _db;
        private IMongoCollection<User> _collection;

        public UserMongoDBRepo(string url, string collection)
        {
            _connectionStirng = new MongoUrl(url);
            _client = new MongoClient(_connectionStirng);
            _db = _client.GetDatabase(_connectionStirng.DatabaseName);
            _collection = _db.GetCollection<User>(collection);
        }

        public async Task<User> Authenticate(string token)
        {
            User u = await _collection.Find<User>(x => x.Token == token).SingleOrDefaultAsync().ConfigureAwait(false);

            if (u == null)
                throw new UserNotFoundException();

            return u;
        }
    }
}
