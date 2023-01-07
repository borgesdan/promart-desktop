using System.Windows.Controls;

namespace Promart.Core
{
    public static class PanelExtensions
    {
        public static void TrimAllTextBox(this Panel panel)
        {
            var children = panel.Children;

            foreach (var child in children)
            {
                if (child is TextBox textbox)
                    textbox.Text = textbox.Text.Trim();
            }
        }
    }
}
