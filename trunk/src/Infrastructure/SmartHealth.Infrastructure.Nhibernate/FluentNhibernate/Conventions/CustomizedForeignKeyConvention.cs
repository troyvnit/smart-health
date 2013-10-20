using System;
using FluentNHibernate.Conventions;

namespace SmartHealth.Infrastructure.Nhibernate.FluentNhibernate.Conventions
{
    public class CustomizedForeignKeyConvention : ForeignKeyConvention
    {
        protected override string GetKeyName(FluentNHibernate.Member property, Type type)
        {
            return type.Name + "Id";
        }
    }
}
