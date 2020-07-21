using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExampleProject.Model.Common;
using ExampleProject.Model;
using ExampleProject.Repository.Common;
using ExampleProject.Repository;
using ExampleProject.Service.Common;
using Autofac;

namespace ExampleProject.Service
{
    public class PersonService : IPersonService
    {
        private IPersonRepository RepositoryResolver { get; set; }
        public PersonService (IPersonRepository personRepository)
        {
            this.RepositoryResolver = personRepository;
        }
        public async Task<bool> AddPerson(PersonInfo person)
        {
            return await RepositoryResolver.PostPerson(person.FirstName, person.LastName, person.PAddress, person.Email);
        }

        public async Task<bool> DeletePerson(int id)
        {
            return await RepositoryResolver.DeletePerson(id);
        }

        public async Task<List<IPerson>> GetPeople()
        {
            return await RepositoryResolver.GetPeople(); ;
        }

        public async Task<IPerson> GetPerson(int id)
        {
            return await RepositoryResolver.GetPerson(id);
        }

        public async Task<bool> UpdatePerson(int id, PersonInfo personInfo)
        {
            return await RepositoryResolver.UpdatePerson(id, personInfo);
        }
    }

    
}
