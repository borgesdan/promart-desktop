using System.Windows.Controls;

namespace Promart.Core
{
    public static class PanelExtensions
    {
        public static void TrimAllTextBox(this Panel panel)
        {
            panel.ForEachVisual(v =>
            {
                if (v is TextBox textbox)
                    textbox.Text = textbox.Text.Trim();
            });           
        }
    }
}
