﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExampleProject.Model.Common;

namespace ExampleProject.Model
{
    public class PersonInfo : IPersonInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PAddress { get; set; }
        public string Email { get; set; }
    }
}
