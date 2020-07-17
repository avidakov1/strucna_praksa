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

namespace ExampleProject.Service
{
    public class PersonService : IPersonService
    {
        private PersonRepository Repository = new PersonRepository();
        public async Task<bool> AddPerson(PersonInfo person)
        {
            return await Repository.PostPerson(person.FirstName, person.LastName, person.PAddress, person.Email);
        }

        public async Task<bool> DeletePerson(int id)
        {
            return await Repository.DeletePerson(id);
        }

        public async Task<List<Person>> GetPeople()
        {
            return await Repository.GetPeople();
        }

        public async Task<Person> GetPerson(int id)
        {
            return await Repository.GetPerson(id);
        }

        public async Task<bool> UpdatePerson(int id, PersonInfo personInfo)
        {
            return await Repository.UpdatePerson(id, personInfo);
        }
    }

    
}
