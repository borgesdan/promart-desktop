using System;
using System.Linq;
using System.Windows.Controls;

namespace Promart.Core
{
    public static class ComboBoxExtensions
    {
        public static void AddEnum<TEnum>(this ComboBox value) where TEnum : struct, Enum
        {
            Enum.GetValues<TEnum>()
                .ToList()
                .ForEach(e => value.Items.Add(new EnumCapsule(e)));
        }
    }
}
