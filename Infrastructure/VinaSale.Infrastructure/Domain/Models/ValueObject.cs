using System;

namespace VinaSale.Infrastructure.Domain.Models
{
    [Serializable]
    public abstract class ValueObject : ValidatableObject
    {
        public static bool operator ==(ValueObject valueObject1, ValueObject valueObject2)
        {
            if ((object)valueObject1 == null)
            {
                return (object)valueObject2 == null;
            }

            return valueObject1.Equals(valueObject2);
        }

        public static bool operator !=(ValueObject valueObject1, ValueObject valueObject2)
        {
            return !(valueObject1 == valueObject2);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}