using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventRepo
{
    public class MongoDBRepo : IEventRepo
    {
        public bool Save(EventLibrary.Event e)
        {
            throw new NotImplementedException();
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
