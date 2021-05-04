using System;

namespace OLT.Core
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class UniqueIdAttribute : Attribute
    {
        public Guid UniqueId { get; private set; }

        private UniqueIdAttribute() { }
        public UniqueIdAttribute(string uniqueId)
        {
            // Just parse it - things should blow up if it doesn't work.
            this.UniqueId = Guid.Parse(uniqueId);
        }

        public static Guid FromType(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            var possibles = type.GetInheritedCustomAttributes<UniqueIdAttribute>();
            if (possibles == null || possibles.Count == 0) return Guid.Empty;
            return possibles[0].UniqueId;
        }

        public static Guid From<T>()
        {
            return FromType(typeof(T));
        }
    }
}