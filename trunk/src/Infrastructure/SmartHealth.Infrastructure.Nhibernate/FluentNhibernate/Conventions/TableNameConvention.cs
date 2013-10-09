using FluentNHibernate.Conventions;

using Inflector;

namespace VinaSale.Infrastructure.Nhibernate.FluentNhibernate.Conventions
{
    public class TableNameConvention : IClassConvention
    {
        public void Apply(FluentNHibernate.Conventions.Instances.IClassInstance instance)
        {
            var module = instance.EntityType.Namespace.Split('.');
            instance.Table(string.Concat(module[1], "_", instance.EntityType.Name.Pluralize()));
        }
    }
}
