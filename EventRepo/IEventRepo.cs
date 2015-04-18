using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventLibrary;

namespace EventRepo
{
    public interface IEventRepo
    {
        public bool Save(Event e);
        public Event FindById(int id);
        public List<Event> FindByDate(DateTime date);
        public List<Event> FindByToken(string token);
        public List<Event> FindAll();
    }
}
