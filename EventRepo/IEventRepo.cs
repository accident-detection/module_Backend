using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using EventLibrary;
using System.Net;
using MongoDB.Bson;

namespace EventRepo
{
    public interface IEventRepo
    {
        Task<string> Save(Event e);
        Task<Event> FindById(string id);
        List<Event> FindByDate(DateTime date);
        List<Event> FindByToken(string token);
        Task<List<Event>> FindAll();
    }
}
