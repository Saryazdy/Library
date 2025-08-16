using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Common
{
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object?> GetEqualityComponents();

        public override bool Equals(object? obj)
        {
            if (obj is null || obj.GetType() != GetType()) return false;

            var other = (ValueObject)obj;

            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Aggregate(0, (hash, obj) => (hash * 397) ^ (obj?.GetHashCode() ?? 0));
        }

        public static bool operator ==(ValueObject? a, ValueObject? b)
            => a is null && b is null || a is not null && a.Equals(b);

        public static bool operator !=(ValueObject? a, ValueObject? b) => !(a == b);
    }
}
