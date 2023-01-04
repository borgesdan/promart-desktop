using System;
using System.ComponentModel;
using System.Reflection;

namespace Promart.Core
{
    public static class EnumExtensions
    {
        public static string Description(this Enum value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            var attribute = value.GetCustomAttribute<DescriptionAttribute>();

            if (attribute == null)
                return value.ToString();

            return attribute.Description;
        }

        public static T GetCustomAttribute<T>(this Enum value) where T : Attribute
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            var fi = value.GetType().GetField(value.ToString());

            return fi.GetCustomAttribute<T>();
        }
    }    
}
