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
using UserRepo;
using UserLibrary;
using MongoDB.Bson;

namespace module_Backend.Controllers
{
    public class EventsController : ApiController
    {
        private static IEventRepo _eventRepo;
        private static IUserRepo _userRepo;
        public EventsController()
        {
            _eventRepo = new EventMongoDBRepo("mongodb://172.16.0.7/adDb", "events");
            _userRepo = new UserMongoDBRepo("mongodb://172.16.0.7/adDb", "users");
        }

        // GET api/events
        public async Task<List<Event>> Get()
        {
            return await _eventRepo.FindAll();
        }

        // GET api/events/5
        public async Task<Event> Get(string id)
        {
            Event e = await _eventRepo.FindById(id);

            return e;
        }

        // POST api/events
        public async Task<HttpResponseMessage> Post(JObject input)
        {
            User authedUser;
            HttpResponseMessage hrm = new HttpResponseMessage();
            string token = (Request.Headers.GetValues("adb-token")).FirstOrDefault();
            try
            {
                authedUser = await _userRepo.Authenticate(token);
            }
            catch (UserNotFoundException)
            {
                hrm.StatusCode = HttpStatusCode.Forbidden;
                hrm.Content = new StringContent("The user token you used is not registred");
                return hrm;
            }

            DateTime time = DateTime.UtcNow;
            int adCode = (int)input["adCode"];
            int gpsCode = (int)input["gpsCode"];
            double lat = (double)input["lat"];
            double lng = (double)input["lng"];
            double speed = (double)input["speed"];


            Event postedEvent = new Event(time, adCode, gpsCode, lat, lng, speed, authedUser.ID);

            var id = await _eventRepo.Save(postedEvent);

            
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
