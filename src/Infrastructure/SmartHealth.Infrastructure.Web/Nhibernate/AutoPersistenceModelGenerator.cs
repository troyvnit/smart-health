using System;

using FluentNHibernate.Automapping;
using FluentNHibernate.Conventions;


using SmartHealth.Infrastructure.Domain.Models;
using SmartHealth.Infrastructure.Nhibernate.FluentNhibernate;
using SmartHealth.Infrastructure.Nhibernate.FluentNhibernate.Conventions;
using SmartHealth.Core.Domain.Models;

namespace SmartHealth.Infrastructure.Web.Nhibernate
{
    using SmartHealth.Core.Infrastructure.Nhibernate.Mapping;
    using SmartHealth.Box.Domain.Models;
    using SmartHealth.Box.Infrastructure.Nhibernate.Mapping;

    public class AutoPersistenceModelGenerator
    {
        public AutoPersistenceModel Generate()
        {
            return AutoMap.Assemblies(new AutomappingConfiguration(), new[]
                                                                          {
                                                                              typeof (User).Assembly
                                                                              , typeof (Product).Assembly

                                                                          })
                .IgnoreBase<Entity>()
                .IgnoreBase(typeof (EntityWithTypedId<>))
                .Conventions.Setup(GetConventions())
                .UseOverridesFromAssemblyOf<AutoPersistenceModelGenerator>()
                .UseOverridesFromAssemblyOf<UserMapping>()
                .UseOverridesFromAssemblyOf<ArticleMapping>();
        }

        private Action<IConventionFinder> GetConventions()
        {
            return c =>
                {
                    c.Add<TableNameConvention>();
                    c.Add<PrimaryKeyConvention>();
                    c.Add<ColumnLengthConvention>();
                    c.Add<ColumnNullConvention>();
                    c.Add<HasManyConvention>();
                    c.Add<HasManyToManyConvention>();
                    c.Add<CustomizedForeignKeyConvention>();
                };
        }
    }
}