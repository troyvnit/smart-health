using System.Reflection;
using System.Web.Mvc;

using Autofac;
using Autofac.Integration.Mvc;

using AutofacContrib.CommonServiceLocator;

using Microsoft.Practices.ServiceLocation;

using VinaSale.Infrastructure.Domain.DataInterfaces;
using VinaSale.Infrastructure.Nhibernate;

namespace VinaSale.Infrastructure.Web.Bootstrap
{
    public class ComponentRegistrar
    {
        public static void RegisterComponents(string webAssembly)
        {
            var builder = new ContainerBuilder();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
            builder.RegisterGeneric(typeof(RepositoryWithTypedId<,>)).As(typeof(IRepositoryWithTypedId<,>));
            
            builder.RegisterAssemblyTypes(Assembly.Load("VinaSale.Core")).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(Assembly.Load("VinaSale.QoL")).AsImplementedInterfaces();

            builder.RegisterControllers(Assembly.Load(webAssembly));

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(container));
        }
    }
}
