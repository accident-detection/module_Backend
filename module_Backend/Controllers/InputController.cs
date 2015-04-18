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

namespace module_Backend.Controllers
{
    public class InputController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public async Task<HttpResponseMessage> Post(JObject input)
        {
            var token = (Request.Headers.GetValues("adb-token")).FirstOrDefault();

            Event postedEvent = new Event((double)input["lat"], (double)input["log"], (int)input["adCode"], token);

            IEventRepo repo = new MongoDBRepo("mongodb://unicorn.d.h/adDb");

            return await repo.Save(postedEvent);
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