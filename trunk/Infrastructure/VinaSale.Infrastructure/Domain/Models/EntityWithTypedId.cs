using System;
using System.Xml.Serialization;

namespace VinaSale.Infrastructure.Domain.Models
{
    [Serializable]
    public abstract class EntityWithTypedId<TId> : ValidatableObject, IEntityWithTypedId<TId>
    {
        /// <summary>
        ///     To help ensure hashcode uniqueness, a carefully selected random number multiplier 
        ///     is used within the calculation.  Goodrich and Tamassia's Data Structures and
        ///     Algorithms in Java asserts that 31, 33, 37, 39 and 41 will produce the fewest number
        ///     of collissions.  See http://computinglife.wordpress.com/2008/11/20/why-do-hash-functions-use-prime-numbers/
        ///     for more information.
        /// </summary>
        private const int HashMultiplier = 31;

        private int? cachedHashcode;

        /// <summary>
        ///     Id may be of type string, int, custom type, etc.
        ///     Setter is protected to allow unit tests to set this property via reflection and to allow 
        ///     domain objects more flexibility in setting this for those objects with assigned Ids.
        ///     It's virtual to allow NHibernate-backed objects to be lazily loaded.
        /// 
        ///     This is ignored for XML serialization because it does not have a public setter (which is very much by design).
        ///     See the FAQ within the documentation if you'd like to have the Id XML serialized.
        /// </summary>
        [XmlIgnore]
        public virtual TId Id { get; protected set; }

        public override bool Equals(object obj)
        {
            var compareTo = obj as EntityWithTypedId<TId>;

            if (ReferenceEquals(this, compareTo))
            {
                return true;
            }

            if (compareTo == null || !GetType().Equals(compareTo.GetTypeUnproxied()))
            {
                return false;
            }

            if (HasSameNonDefaultIdAs(compareTo))
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            if (cachedHashcode.HasValue)
            {
                return cachedHashcode.Value;
            }

            if (IsTransient())
            {
                cachedHashcode = base.GetHashCode();
            }
            else
            {
                unchecked
                {
                    // It's possible for two objects to return the same hash code based on 
                    // identically valued properties, even if they're of two different types, 
                    // so we include the object's type in the hash calculation
                    var hashCode = GetType().GetHashCode();
                    cachedHashcode = (hashCode * HashMultiplier) ^ Id.GetHashCode();
                }
            }

            return cachedHashcode.Value;
        }

        /// <summary>
        ///     Transient objects are not associated with an item already in storage.  For instance,
        ///     a Customer is transient if its Id is 0.  It's virtual to allow NHibernate-backed 
        ///     objects to be lazily loaded.
        /// </summary>
        public virtual bool IsTransient()
        {
            return Id == null || Id.Equals(default(TId));
        }

        /// <summary>
        ///     When NHibernate proxies objects, it masks the type of the actual entity object.
        ///     This wrapper burrows into the proxied object to get its actual type.
        /// 
        ///     Although this assumes NHibernate is being used, it doesn't require any NHibernate
        ///     related dependencies and has no bad side effects if NHibernate isn't being used.
        /// 
        ///     Related discussion is at http://groups.google.com/group/sharp-architecture/browse_thread/thread/ddd05f9baede023a ...thanks Jay Oliver!
        /// </summary>
        protected virtual Type GetTypeUnproxied()
        {
            return GetType();
        }

        /// <summary>
        ///     Returns true if self and the provided entity have the same Id values 
        ///     and the Ids are not of the default Id value
        /// </summary>
        private bool HasSameNonDefaultIdAs(EntityWithTypedId<TId> compareTo)
        {
            return !IsTransient() &&
                   !compareTo.IsTransient() &&
                   Id.Equals(compareTo.Id);
        }
    }
}
