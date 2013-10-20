using System.Reflection;
using System.Web.Mvc;

using Autofac;
using Autofac.Integration.Mvc;

using AutofacContrib.CommonServiceLocator;

using Microsoft.Practices.ServiceLocation;
using SmartHealth.Infrastructure.Bussiness;
using SmartHealth.Infrastructure.Domain.DataInterfaces;
using SmartHealth.Infrastructure.Nhibernate;

namespace SmartHealth.Infrastructure.Web.Bootstrap
{
    public class ComponentRegistrar
    {
        public static void RegisterComponents(string webAssembly)
        {
            var builder = new ContainerBuilder();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
            builder.RegisterGeneric(typeof(RepositoryWithTypedId<,>)).As(typeof(IRepositoryWithTypedId<,>));
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>));
            builder.RegisterGeneric(typeof(ServiceWithTypedId<,>)).As(typeof(IServiceWithTypedId<,>));
            
            builder.RegisterAssemblyTypes(Assembly.Load("SmartHealth.Core")).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(Assembly.Load("SmartHealth.Box")).AsImplementedInterfaces();

            builder.RegisterControllers(Assembly.Load(webAssembly));

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(container));
        }
    }
}
