using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using ExampleProject.Model.Common;
using ExampleProject.Service;
using ExampleProject.Model;

namespace ExampleProject.Webapi.Controllers
{    
    public class ReadWriteController : ApiController
    {
        private PersonService PService = new PersonService();
        [HttpGet]
        [Route("api/getallPerson")]
        public List<IPerson> Get()
        {
            return PService.GetPeople();
        }
        [HttpGet]
        [Route("api/getonePerson/{id:int}")]
        public IPerson Get(int id)
        {
            return PService.GetPerson(id);
        }
        [Route("api/addPerson")]
        public bool Post([FromBody]PersonInfo value)
        {
            return PService.AddPerson(value);
        }

        [Route("api/updatePerson/{id:int}")]
        public bool Put([FromUri] int id, [FromBody]PersonInfo value)
        {
            return PService.UpdatePerson(id, value);
        }

        [Route("api/remove/{id:int}")]
        public bool Delete([FromUri] int id)
        {
            return PService.DeletePerson(id);
        }
    }
    
}
