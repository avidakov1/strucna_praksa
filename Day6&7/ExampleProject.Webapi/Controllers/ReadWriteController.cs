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
using ExampleProject.Service.Common;
using ExampleProject.Model;
using AutoMapper;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;

namespace ExampleProject.Webapi.Controllers
{    
    public class ReadWriteController : ApiController
    {
        private IPersonService PersonServiceResolver { get; set; }
        public ReadWriteController(IPersonService personService) {
            this.PersonServiceResolver = personService;
        }
        [HttpGet]
        [Route("api/Person")]
        public async Task<HttpResponseMessage> Get()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Person, PersonInfo>());
            var People = new List<Person>(await PersonServiceResolver.GetPeople());
            var mapper = new Mapper(config);
            var PeopleREST = new List<PersonInfo>();
            foreach(Person person in People)
            {
                var PersonRest = mapper.Map<PersonInfo>(person);
                PeopleREST.Add(PersonRest);
            }
            return Request.CreateResponse(HttpStatusCode.OK, People);
        }
        [HttpGet]
        [Route("api/Person/{id:Guid}")]
        public async Task<HttpResponseMessage> Get(Guid id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Person, PersonInfo>());
            Person MyPerson = await PersonServiceResolver.GetPerson(id);
            if (MyPerson == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No Person Found.");
            }
            var mapper = new Mapper(config);
            return Request.CreateResponse(HttpStatusCode.OK, mapper.Map<PersonInfo>(MyPerson));
        }
        [HttpPost]
        [Route("api/Person")]
        public async Task<HttpResponseMessage> Post([FromBody]PersonInfo value)
        {
            if (await PersonServiceResolver.AddPerson(value))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "OK");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad Request");
        }
        [HttpPut]
        [Route("api/Person/{id:Guid}")]
        public async Task<HttpResponseMessage> Put([FromUri] Guid id, [FromBody]PersonInfo value)
        {
            if (await PersonServiceResolver.UpdatePerson(id, value))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "OK");
            };
            return Request.CreateResponse(HttpStatusCode.NotFound, "Not Found");
        }

        [HttpDelete]
        [Route("api/Person/{id:Guid}")]
        public async Task<HttpResponseMessage> Delete([FromUri] Guid id)
        {
            if (await PersonServiceResolver.DeletePerson(id))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "OK");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad Request");
        }
    }
}
