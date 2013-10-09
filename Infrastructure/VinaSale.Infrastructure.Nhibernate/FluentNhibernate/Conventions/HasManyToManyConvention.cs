using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace VinaSale.Infrastructure.Nhibernate.FluentNhibernate.Conventions
{
    public class HasManyToManyConvention : IHasManyToManyConvention
    {
        public void Apply(IManyToManyCollectionInstance instance)
        {
            instance.Cascade.SaveUpdate();
        }
    }
}
