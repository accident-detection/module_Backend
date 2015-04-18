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
    public interface IEventRepo
    {
        Task<HttpResponseMessage> Save(Event e);
        Event FindById(int id);
        List<Event> FindByDate(DateTime date);
        List<Event> FindByToken(string token);
        List<Event> FindAll();
    }
}
