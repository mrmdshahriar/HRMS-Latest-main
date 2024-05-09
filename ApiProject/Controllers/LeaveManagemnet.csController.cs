using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using TalenAPI.Models;

//using TalentBAL;

namespace TalenAPI.Controllers
{
    public class LeaveManagemnetController : ApiController
    {
 
        ResponseModel ObjResponseModel = new ResponseModel();
        // GET api/values

        [HttpPost]
        [Route("api/leave/LeaveTypevvbcs")]
        public HttpResponseMessage GetData()
        {
            var ddd = new string[] { "value1", "value2" };
            return Request.CreateResponse(ddd);
        }

        // GET api/values/5

        
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
     
        public void Post()
        {
            
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
       
        }
}
