using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExampleProject.Model.Common;
using ExampleProject.Model;

namespace ExampleProject.Repository.Common
{
    public interface IPersonRepository
    {
        #region Methods
        List<IPerson> GetPeople();
        IPerson GetPerson(int id);
        bool PostPerson(string firstName, string lastName, string pAddress, string email);
        bool UpdatePerson(int id, PersonInfo personInfo);
        bool DeletePerson(int id);

        #endregion Methods

    }

}