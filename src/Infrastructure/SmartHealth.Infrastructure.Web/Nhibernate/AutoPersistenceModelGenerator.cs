using System;

using FluentNHibernate.Automapping;
using FluentNHibernate.Conventions;


using VinaSale.Infrastructure.Domain.Models;
using VinaSale.Infrastructure.Nhibernate.FluentNhibernate;
using VinaSale.Infrastructure.Nhibernate.FluentNhibernate.Conventions;
using VinaSale.Core.Domain.Models;

namespace VinaSale.Infrastructure.Web.Nhibernate
{
    using VinaSale.Core.Infrastructure.Nhibernate.Mapping;
    using VinaSale.Box.Domain.Models;
    using VinaSale.Box.Infrastructure.Nhibernate.Mapping;

    public class AutoPersistenceModelGenerator
    {
        public AutoPersistenceModel Generate()
        {
            return AutoMap.Assemblies(new AutomappingConfiguration(), new[]
                {
                    typeof(User).Assembly
                    //,typeof(HealthSurvey).Assembly
                    
                })
                .IgnoreBase<Entity>()
                .IgnoreBase(typeof(EntityWithTypedId<>))
                .Conventions.Setup(GetConventions())
                .UseOverridesFromAssemblyOf<AutoPersistenceModelGenerator>()
                .UseOverridesFromAssemblyOf<UserMapping>();
                //.UseOverridesFromAssemblyOf<HealthSurveyMapping>();
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