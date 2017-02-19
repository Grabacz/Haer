using Autofac;
using Autofac.Integration.WebApi;
using DG.Haer.Data;
using DG.Haer.Service;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace DG.Haer.Api
{
    public class AutofacConfig
    {
        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        public static IContainer RegisterServices(ContainerBuilder builder)
        {

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder
                .RegisterType<DbProvider>()
                .As<IDbProvider>()
                .InstancePerRequest();

            builder
                .RegisterType<DbFactory>()
                .As<IDbFactory>()
                .InstancePerRequest();

            builder
                .RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerRequest();

            builder
                .RegisterType<SalaryStrategyFactory>()
                .As<ISalaryStrategyFactory>()
                .InstancePerRequest();

            builder
                .RegisterAssemblyTypes(typeof(_DataAssemblyMarker).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerRequest();

            builder
                .RegisterAssemblyTypes(typeof(_ServiceAssemblyMarker).Assembly)
               .Where(t => t.Name.EndsWith("Service"))
               .AsImplementedInterfaces().InstancePerRequest();

            Container = builder.Build();
            return Container;
        }
    }
}


//builder.RegisterGeneric(typeof(EntityBaseRepository<>))
//       .As(typeof(IEntityBaseRepository<>))
//       .InstancePerRequest();

//// Services
//builder.RegisterType<EncryptionService>()
//    .As<IEncryptionService>()
//    .InstancePerRequest();

//builder.RegisterType<MembershipService>()
//    .As<IMembershipService>()
//    .InstancePerRequest();
//// EF HomeCinemaContext
//builder.RegisterType<HomeCinemaContext>()
//       .As<DbContext>()
//       .InstancePerRequest();
