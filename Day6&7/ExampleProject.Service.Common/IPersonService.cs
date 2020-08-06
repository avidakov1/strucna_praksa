using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExampleProject.Model.Common;
using ExampleProject.Model;

namespace ExampleProject.Service.Common
{
    public interface IPersonService
    {
        Task<List<Person>> GetPeople();
        Task<Person> GetPerson(Guid id);
        Task<bool> AddPerson(PersonInfo person);
        Task<bool> UpdatePerson(Guid id, PersonInfo personInfo);
        Task<bool> DeletePerson(Guid id);
    }
}
