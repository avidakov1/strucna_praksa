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
        public bool AddPerson(PersonInfo person)
        {
            return Repository.PostPerson(person.FirstName, person.LastName, person.PAddress, person.Email);
        }

        public bool DeletePerson(int id)
        {
            return Repository.DeletePerson(id);
        }

        public List<IPerson> GetPeople()
        {
            return Repository.GetPeople();
        }

        public IPerson GetPerson(int id)
        {
            return Repository.GetPerson(id);
        }

        public bool UpdatePerson(int id, PersonInfo personInfo)
        {
            return Repository.UpdatePerson(id, personInfo);
        }
    }
}
