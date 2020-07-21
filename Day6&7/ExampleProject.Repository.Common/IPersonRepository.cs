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
        Task<List<IPerson>> GetPeople();
        Task<IPerson> GetPerson(int id);
        Task<bool> PostPerson(string firstName, string lastName, string pAddress, string email);
        Task<bool> UpdatePerson(int id, PersonInfo personInfo);
        Task<bool> DeletePerson(int id);

        #endregion Methods

    }

}