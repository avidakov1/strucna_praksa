using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleProject.Model.Common
{
    public interface IPersonInfo
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        DateTime DOB { get; set; }
    }
}
