using FluentNHibernate.Conventions;
using System.ComponentModel.DataAnnotations;
using FluentNHibernate.Conventions.Instances;

namespace VinaSale.Infrastructure.Nhibernate.FluentNhibernate.Conventions
{
    public class ColumnLengthConvention : AttributePropertyConvention<StringLengthAttribute>
    {
        protected override void Apply(StringLengthAttribute attribute, IPropertyInstance instance)
        {
            // override the default column length
            int maxLength = 4001;
            if (attribute.MaximumLength >= 1 && attribute.MaximumLength <= 4000)
            {
                maxLength = (int)(attribute.MaximumLength);
            }

            instance.Length(maxLength);
        }
    }

    public class ColumnNullConvention : IPropertyConvention
    {
        public void Apply(IPropertyInstance instance)
        {
            if (instance.Property.MemberInfo.IsDefined(typeof(RequiredAttribute), false))
                instance.Not.Nullable();
        }
    }
}
