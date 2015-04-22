using EventLibrary;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventRepo
{
    public class EventMongoDBRepo : IEventRepo
    {
        private MongoClient _client;
        private MongoUrl _connectionStirng;
        private IMongoDatabase _db;
        private IMongoCollection<Event> _collection;

        public EventMongoDBRepo(string url, string collection)
        {
            _connectionStirng = new MongoUrl(url);
            _client = new MongoClient(_connectionStirng);
            _db = _client.GetDatabase(_connectionStirng.DatabaseName);
            _collection = _db.GetCollection<Event>(collection);
        }

        public async Task<string> Save(Event e)
        {
            await _collection.InsertOneAsync(e);

            return e.ID.ToString(); ;
        }

        public async Task<Event> FindById(string id)
        {
            Event e = await _collection.Find<Event>(x => x.ID == id).FirstAsync().ConfigureAwait(false);

            return e;
        }

        public List<EventLibrary.Event> FindByDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public List<EventLibrary.Event> FindByToken(string token)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Event>> FindAll()
        {
            var filter = new BsonDocument();
            List<Event> list = await _collection.Find<Event>(filter).ToListAsync().ConfigureAwait(false);

            return await Task<List<Event>>.FromResult(list);
        }
    }
}
