using System;

namespace Promart.Core
{
    public class EnumCapsule<T> where T: struct, Enum
    {
        public T Value { get; set; }

        public EnumCapsule(T value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.Description() ?? string.Empty;
        }
    }
}