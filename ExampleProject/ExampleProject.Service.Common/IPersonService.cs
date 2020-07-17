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
        Task<Person> GetPerson(int id);
        Task<bool> AddPerson(PersonInfo person);
        Task<bool> UpdatePerson(int id, PersonInfo personInfo);
        Task<bool> DeletePerson(int id);
    }
}
