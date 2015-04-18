using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using EventLibrary;
using System.Net;

namespace EventRepo
{
    public class MongoDBRepo : IEventRepo
    {
        private MongoClient _client;
        private MongoUrl _connectionStirng;
        private IMongoDatabase _db;
        private IMongoCollection<Event> _collection;

        public MongoDBRepo(string url)
        {
            _connectionStirng = new MongoUrl(url);
            _client = new MongoClient(_connectionStirng);
            _db = _client.GetDatabase(_connectionStirng.DatabaseName);
            _collection = _db.GetCollection<Event>("events");
        }

        public async Task<HttpResponseMessage> Save(Event e)
        {
            await _collection.InsertOneAsync(e);

            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        public EventLibrary.Event FindById(int id)
        {
            throw new NotImplementedException();
        }

        public List<EventLibrary.Event> FindByDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public List<EventLibrary.Event> FindByToken(string token)
        {
            throw new NotImplementedException();
        }

        public List<EventLibrary.Event> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
