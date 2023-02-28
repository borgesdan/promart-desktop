using Promart.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
using System.Windows.Shapes;

namespace Promart.Windows
{
    /// <summary>
    /// Interaction logic for BackupSelectWindow.xaml
    /// </summary>
    public partial class BackupSelectWindow : Window
    {
        public BackupSelectWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var destination = System.IO.Path.Combine(Environment.CurrentDirectory, "Backups");
            var directory = new DirectoryInfo(destination);

            directory.GetFiles().ToList().ForEach(f =>
            {
                if (f.Extension != ".bak")
                    return;

                MainListView.Items.Add(f.Name);
            });
        }

        private async void Select_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = MainListView.SelectedItem;

            if (selectedItem == null)
            {
                Close();
                return;
            }
                
            var config = ConfigManager.Open();
            string fileInfo = selectedItem as string;
            var destionationFile = System.IO.Path.Combine(Environment.CurrentDirectory, "Backups", $"{fileInfo}");

            var result = await DataBaseManager.RestoreDatabaseAsync("PromartDesktop", config.ConnectionStrings.Default, destionationFile);

            if(result != null)
                Error.ShowDatabaseError($"Ocorreu um erro ao tentar restaurar o backup do banco de dados.", result);
        }
    }
}
