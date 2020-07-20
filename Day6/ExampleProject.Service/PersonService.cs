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
using Autofac;

namespace ExampleProject.Service
{
    public class PersonService : IPersonService
    {
        public async Task<bool> AddPerson(PersonInfo person)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<PersonRepository>().As<IPersonRepository>();
            var container = containerBuilder.Build();
            var RepositoryResolver = container.Resolve<IPersonRepository>();
            var retVal = await RepositoryResolver.PostPerson(person.FirstName, person.LastName, person.PAddress, person.Email);
            container.DisposeAsync();
            return retVal;
        }

        public async Task<bool> DeletePerson(int id)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<PersonRepository>().As<IPersonRepository>();
            var container = containerBuilder.Build();
            var RepositoryResolver = container.Resolve<IPersonRepository>();
            var retVal = await RepositoryResolver.DeletePerson(id);
            container.DisposeAsync();
            return retVal;
        }

        public async Task<List<IPerson>> GetPeople()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<PersonRepository>().As<IPersonRepository>();
            var container = containerBuilder.Build();
            var RepositoryResolver = container.Resolve<IPersonRepository>();
            var retVal = await RepositoryResolver.GetPeople();
            container.DisposeAsync();
            return retVal;
        }

        public async Task<IPerson> GetPerson(int id)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<PersonRepository>().As<IPersonRepository>();
            var container = containerBuilder.Build();
            var RepositoryResolver = container.Resolve<IPersonRepository>();
            var retVal = await RepositoryResolver.GetPerson(id);
            container.DisposeAsync();
            return retVal;
        }

        public async Task<bool> UpdatePerson(int id, PersonInfo personInfo)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<PersonRepository>().As<IPersonRepository>();
            var container = containerBuilder.Build();
            var RepositoryResolver = container.Resolve<IPersonRepository>();
            var retVal = await RepositoryResolver.UpdatePerson(id, personInfo);
            container.DisposeAsync();
            return retVal;
        }
    }

    
}
