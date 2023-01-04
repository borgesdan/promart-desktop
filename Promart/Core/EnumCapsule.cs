using System;

namespace Promart.Core
{
    public class EnumCapsule
    {
        public Enum? Value { get; set; }
        
        public EnumCapsule(Enum value) 
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value?.Description() ?? string.Empty;
        }
    }
}