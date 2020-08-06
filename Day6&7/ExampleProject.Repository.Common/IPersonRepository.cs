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
        Task<List<Person>> GetPeople();
        Task<Person> GetPerson(Guid id);
        Task<bool> PostPerson(PersonInfo personInfo);
        Task<bool> UpdatePerson(Guid id, PersonInfo personInfo);
        Task<bool> DeletePerson(Guid id);

        #endregion Methods

    }

}