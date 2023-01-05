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
                .ForEach(e => value.Items.Add(new EnumCapsule<TEnum>(e)));
        }

        public static TEnum? GetEnumInSelectedItem<TEnum>(this ComboBox value) where TEnum: struct, Enum
        {
            var item = value.SelectedItem;

            if (item is not null and EnumCapsule<TEnum>)
                return ((EnumCapsule<TEnum>)item).Value;

            return null;
        }
    }
}
