using System;
using System.Linq;

using FluentNHibernate;
using FluentNHibernate.Automapping;

using SmartHealth.Infrastructure.Domain.Models;

namespace SmartHealth.Infrastructure.Nhibernate.FluentNhibernate
{
    public class AutomappingConfiguration: DefaultAutomappingConfiguration
    {
        public override bool AbstractClassIsLayerSupertype(Type type)
        {
            return type == typeof(EntityWithTypedId<>) || type == typeof(Entity);
        }

        public override bool IsId(Member member)
        {
            return member.Name == "Id";
        }

        public override bool ShouldMap(Type type)
        {
            return
                type.GetInterfaces().Any(
                    x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEntityWithTypedId<>));
        }

        public override bool IsComponent(Type type)
        {
            var result = type.BaseType == typeof(ValueObject);
            return result;
        }

        public override string GetComponentColumnPrefix(Member member)
        {
            return member.PropertyType.Name + "_";
        }

        public override bool ShouldMap(Member member)
        {
            return base.ShouldMap(member) && member.CanWrite;
        }
    }
}
