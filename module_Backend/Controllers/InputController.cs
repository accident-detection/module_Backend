using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EventLibrary;
using System.Web.Mvc;
using EventRepo;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace module_Backend.Controllers
{
    public class InputController : ApiController
    {
        private static IEventRepo _repo;
        static InputController()
        {
            _repo = new MongoDBRepo("mongodb://172.16.0.7/adDb");
        }

        // GET api/<controller>
        public async Task<List<Event>> Get()
        {
            return await _repo.FindAll();
        }

        // GET api/<controller>/5
        public async Task<Event> Get(string id)
        {
            Event e = await _repo.FindById(id);

            return e;
        }

        // POST api/<controller>
        public async Task<HttpResponseMessage> Post(JObject input)
        {
            string token = (Request.Headers.GetValues("adb-token")).FirstOrDefault();
            double lat = (double)input["lat"];
            double log = (double)input["log"];
            int adCode = (int)input["adCode"];

            Event postedEvent = new Event(lat, log, adCode, token);

            var id = await _repo.Save(postedEvent);

            HttpResponseMessage hrm = new HttpResponseMessage();
            hrm.StatusCode = HttpStatusCode.Created;
            hrm.Content = new StringContent(id);

            return hrm;
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}