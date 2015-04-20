using EventLibrary;
using EventRepo;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace module_Backend.Controllers
{
    public class EventsController : ApiController
    {
        private static IEventRepo _repo;
        public EventsController()
        {
            _repo = new MongoDBRepo("mongodb://172.16.0.7/adDb");
        }

        // GET api/events
        public async Task<List<Event>> Get()
        {
            return await _repo.FindAll();
        }

        // GET api/events/5
        public async Task<Event> Get(string id)
        {
            Event e = await _repo.FindById(id);

            return e;
        }

        // POST api/events
        public async Task<HttpResponseMessage> Post(JObject input)
        {
            string token = (Request.Headers.GetValues("adb-token")).FirstOrDefault();
            DateTime time = DateTime.UtcNow;
            int adCode = (int)input["adCode"];
            int gpsCode = (int)input["gpsCode"];
            double lat = (double)input["lat"];
            double lng = (double)input["lng"];
            double speed = (double)input["speed"];


            Event postedEvent = new Event(time, adCode, gpsCode, lat, lng, speed, token);

            var id = await _repo.Save(postedEvent);

            HttpResponseMessage hrm = new HttpResponseMessage();
            hrm.StatusCode = HttpStatusCode.Created;
            hrm.Content = new StringContent(id);

            return hrm;
        }

        // PUT api/events/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/events/5
        public void Delete(int id)
        {
        }
    }
}
