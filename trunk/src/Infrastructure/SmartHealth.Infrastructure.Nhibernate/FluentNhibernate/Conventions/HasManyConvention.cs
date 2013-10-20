using System;
using FluentNHibernate.Conventions;

namespace SmartHealth.Infrastructure.Nhibernate.FluentNhibernate.Conventions
{
    public class HasManyConvention : IHasManyConvention
    {
        public void Apply(FluentNHibernate.Conventions.Instances.IOneToManyCollectionInstance instance)
        {
            instance.Key.ForeignKey(String.Format("{0}_{1}_FK",
                instance.Member.Name, instance.EntityType.Name));

            instance.Cascade.AllDeleteOrphan();
            instance.Inverse();
        }
    }
}
