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

        public static void ApplyOnlyNumbers(this TextBox textbox)
        {
            textbox.TextChanged += (object sender, TextChangedEventArgs e) =>
            {
                textbox.Text = textbox.Text.ApplyOnlyNumber();
                textbox.CaretIndex = textbox.Text.Length;
            };
        }

        public static void ApplyOnlyNumberOrLetter(this TextBox textbox)
        {
            textbox.TextChanged += (object sender, TextChangedEventArgs e) =>
            {
                textbox.Text = textbox.Text.ApplyOnlyNumerOrLetter();
                textbox.CaretIndex = textbox.Text.Length;
            };
        }

        public static void ApplyOnlyLetterOrWhiteSpace(this TextBox textbox)
        {
            textbox.TextChanged += (object sender, TextChangedEventArgs e) =>
            {
                textbox.Text = textbox.Text.ApplyOnlyLetterOrWhiteSpace();
                textbox.CaretIndex = textbox.Text.Length;
            };
        }
    }
}
