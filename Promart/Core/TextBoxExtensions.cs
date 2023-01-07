using System.Windows.Controls;
using System.Windows.Media;

namespace Promart.Core
{
    public static class TextBoxExtensions
    {
        public static void SetErrorLayout(this TextBox textBox)
        {
            textBox.Background = Brushes.Red;
            textBox.Foreground = Brushes.White;
        }
    }
}
