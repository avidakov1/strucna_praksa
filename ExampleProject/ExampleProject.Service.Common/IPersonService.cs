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
        List<IPerson> GetPeople();
        IPerson GetPerson(int id);
        bool AddPerson(PersonInfo person);
        bool UpdatePerson(int id, PersonInfo personInfo);
        bool DeletePerson(int id);
    }
}
