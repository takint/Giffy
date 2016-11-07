using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Giffy.DataAccess;
using Giffy.DataAccess.Infrastructure;
using Giffy.DataAccess.Repositories;
using Giffy.DataAccess.Services;
using Giffy.Entities.Models;

namespace Giffy
{
    public static class AutofacConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<GiffyContext>().As<IDataContext>().InstancePerRequest();
            builder.RegisterType<RepositoryBase<Post>>().As<IRepository<Post>>().InstancePerRequest();
            builder.RegisterType<RepositoryBase<Comment>>().As<IRepository<Comment>>().InstancePerRequest();
            builder.RegisterType<RepositoryBase<Like>>().As<IRepository<Like>>().InstancePerRequest();
            builder.RegisterType<RepositoryBase<League>>().As<IRepository<League>>().InstancePerRequest();
            builder.RegisterType<RepositoryBase<Team>>().As<IRepository<Team>>().InstancePerRequest();
            builder.RegisterType<RepositoryBase<Player>>().As<IRepository<Player>>().InstancePerRequest();
            builder.RegisterType<RepositoryBase<Tag>>().As<IRepository<Tag>>().InstancePerRequest();

            // Repositories
            builder.RegisterAssemblyTypes(typeof(PostRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerRequest();
            // Services
            builder.RegisterAssemblyTypes(typeof(PostService).Assembly)
               .Where(t => t.Name.EndsWith("Service"))
               .AsImplementedInterfaces().InstancePerRequest();

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}