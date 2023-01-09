using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Promart.Controls
{
    /// <summary>
    /// Interaction logic for HeaderControl.xaml
    /// </summary>
    public partial class HeaderControl : UserControl
    {
        public HeaderControl(string title)
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
