using Autofac;
using Autofac.Integration.WebApi;
using GameDataAccessLayer;
using GameDataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace PlayerWebAPI
{
    public static class ContainerConfig
    {
        public static IContainer Container { get; set; }

        public static void Register(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(typeof(Startup).Assembly);

            // Register repositories
            builder.Register(c => { return RepositoryFactory.Create<Character>(DatabaseConfig.CreateConnection); })
                .As<IRepository<Character>>();

            Container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(Container);
        }
    }
}