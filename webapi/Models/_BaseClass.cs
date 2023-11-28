using Test1.Models;

namespace webapi.Models
{
    public abstract class BaseClass : IComparable<BaseClass>, IComparable
    {
        public int Id { get; set; }

        // Neccessary to OrderBy on Custom Type (no Primitive)
        public int CompareTo(BaseClass? other)
        {
            if (Id < other?.Id)
            {
                return -1;
            }
            else if (Id > other?.Id)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int CompareTo(object other)
        {
            return CompareTo(other as BaseClass);
        }



        /// When overriden in a derived class, returns all components of a BaseClass which constitute its identity.
        protected abstract IEnumerable<object> GetEqualityComponents();

        public static bool operator ==(BaseClass a, BaseClass b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }
            return a.Equals(b);
        }

        public static bool operator !=(BaseClass a, BaseClass b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj))
                return true;
            if (object.ReferenceEquals(null, obj))
                return false;
            if (this.GetType() != obj.GetType())
                return false;
            var vo = obj as BaseClass;
            return GetEqualityComponents().SequenceEqual(vo.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }
        
    }
}
