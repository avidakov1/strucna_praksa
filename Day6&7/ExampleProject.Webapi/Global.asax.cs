using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Autofac;
using ExampleProject.Model.Common;
using ExampleProject.Repository.Common;
using ExampleProject.Service.Common;
using ExampleProject.Model;
using ExampleProject.Repository;
using ExampleProject.Service;
using Autofac.Integration.WebApi;
using ExampleProject.Webapi.Controllers;
using System.Reflection;

namespace ExampleProject.Webapi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterModule(new DIPersonRepositoryModule());
            builder.RegisterModule(new DIPersonServiceModule());

            var container = builder.Build();

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
