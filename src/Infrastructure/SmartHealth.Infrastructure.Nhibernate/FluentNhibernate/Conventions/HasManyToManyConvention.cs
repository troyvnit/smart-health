using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace SmartHealth.Infrastructure.Nhibernate.FluentNhibernate.Conventions
{
    public class HasManyToManyConvention : IHasManyToManyConvention
    {
        public void Apply(IManyToManyCollectionInstance instance)
        {
            instance.Cascade.SaveUpdate();
        }
    }
}
