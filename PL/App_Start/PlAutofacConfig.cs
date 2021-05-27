using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using BLL;
using PL.Mapper;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;

namespace PL.App_Start
{
    public class PlAutofacConfig
    {
        private const string _connectionString = "Connect";
        private PlAutofacConfig()
        {
        }

        public static void Run()
        {
            ConfigureContainer();
        }

        public static void ConfigureContainer()
        {
            var connectionString = ConfigurationManager.ConnectionStrings[_connectionString].ConnectionString;

            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModule<AutofacWebTypesModule>();

            builder.RegisterModule(new BllAutofacModule(connectionString));

            builder.Register(ctx => new MapperConfiguration(cfg =>
                cfg.AddProfiles(new List<Profile>
                    {
                        new BllMapper(),
                        new PlMapper(),
                    })))
                .AsSelf().SingleInstance();
            builder.Register(c =>
            {
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
            .As<IMapper>()
            .InstancePerLifetimeScope();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}