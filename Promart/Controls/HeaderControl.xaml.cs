using System.Windows;
using System.Windows.Controls;

namespace Promart.Controls
{
    /// <summary>
    /// Interaction logic for HeaderControl.xaml
    /// </summary>
    public partial class HeaderControl : UserControl
    {
        public HeaderControl(string? title)
        {
            InitializeComponent();

            Title.Content = title;
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Deseja fechar essa aba?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes)
                return;

            var tabItem = (TabItem)Parent;
            var tabControl = (TabControl)tabItem.Parent;

            tabControl.Items.Remove(tabItem);
        }
    }
}
