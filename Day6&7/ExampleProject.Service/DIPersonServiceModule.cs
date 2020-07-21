using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using ExampleProject.Service.Common;
namespace ExampleProject.Service
{
    public class DIPersonServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PersonService>().As<IPersonService>();
        }
    }
}
