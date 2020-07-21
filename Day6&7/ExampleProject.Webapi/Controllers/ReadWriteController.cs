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
        [Route("api/getallPeople")]
        public async Task<HttpResponseMessage> Get()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Person, PersonRestModel>());
            List<IPerson> People = new List<IPerson>(await PersonServiceResolver.GetPeople());
            if (People.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No people found.");
            }
            var mapper = new Mapper(config);
            List<PersonRestModel> PeopleREST = new List<PersonRestModel>();
            foreach(Person person in People)
            {
                PersonRestModel PersonRest = mapper.Map<PersonRestModel>(person);
                PeopleREST.Add(PersonRest);
            }
            return Request.CreateResponse(HttpStatusCode.OK, PeopleREST);
        }
        [HttpGet]
        [Route("api/getonePerson/{id:int}")]
        public async Task<HttpResponseMessage> Get(int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Person, PersonRestModel>());
            IPerson MyPerson = await PersonServiceResolver.GetPerson(id);
            if (MyPerson == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No Person Found.");
            }
            var mapper = new Mapper(config);
            return Request.CreateResponse(HttpStatusCode.OK, mapper.Map<PersonRestModel>(MyPerson));
        }
        [Route("api/addPerson")]
        public async Task<HttpResponseMessage> Post([FromBody]PersonInfo value)
        {
            if (await PersonServiceResolver.AddPerson(value))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "OK");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad Request");
        }

        [Route("api/updatePerson/{id:int}")]
        public async Task<HttpResponseMessage> Put([FromUri] int id, [FromBody]PersonInfo value)
        {
            if (await PersonServiceResolver.UpdatePerson(id, value))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "OK");
            };
            return Request.CreateResponse(HttpStatusCode.NotFound, "Not Found");
        }

        [Route("api/remove/{id:int}")]
        public async Task<HttpResponseMessage> HttpResponseMessage([FromUri] int id)
        {
            if (await PersonServiceResolver.DeletePerson(id))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "OK");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad Request");
        }
    }
    class PersonRestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PAddress { get; set; }
        public string Email { get; set; }
        public PersonRestModel(string firstName, string lastName, string pAddress, string email)
        {
            FirstName = firstName; LastName = lastName; PAddress = pAddress; Email = email;
        }
    }
}
