using Promart.Database.Context;
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
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Promart.Pages;
using Promart.Controls;
using Promart.Database.Entities;
using System.Reflection.PortableExecutable;
using Microsoft.Win32;
using Promart.Core;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using Promart.Windows;
using Promart.Services;

namespace Promart
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainWindowService _mainWindowService;

        public MainWindow()
        {   
            InitializeComponent();
            _mainWindowService = new MainWindowService(this);
        }

        private void StudentRegister_Click(object sender, RoutedEventArgs e)
            => _mainWindowService.OpenStudentRegistryTab();

        private void WorkshopRegister_Click(object sender, RoutedEventArgs e)
            => _mainWindowService.OpenWorkshopRegistryTab();

        private void StudentFilter_Click(object sender, RoutedEventArgs e)
            => _mainWindowService.OpenStudentFilterTab();

        private void WorkshopList_Click(object sender, RoutedEventArgs e)
            => _mainWindowService.OpenWorkshopList();

        private void StudentList_Click(object sender, RoutedEventArgs e)
            => _mainWindowService.OpenStudentListTab();

        public void NavigateToStudentRegisterPage(int studentId, string? studentName, NavigationUIVisibility navigationUIVisibility = NavigationUIVisibility.Visible)
            => _mainWindowService.NavigateToStudentRegistryPage(studentId, studentName, navigationUIVisibility);

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
            => _mainWindowService.Exit();

        private async void Backup_Click(object sender, RoutedEventArgs e)
        {   
            var now = DateTime.Now;

            var year = now.Year.ToString();
            var month = now.Month < 10 ? $"0{now.Month}" : now.Month.ToString();
            var day = now.Day < 10 ? $"0{now.Day}" : now.Day.ToString();
            
            var timeString = DateTime.Now.ToLongTimeString().Replace(":", "-");

            this.IsEnabled = false;

            var configuration = ConfigManager.Open();
            var destionationFile = System.IO.Path.Combine(Environment.CurrentDirectory, "Backups", $"{year}-{month}-{day}-{timeString}-promartdesktop.bak");

            var result = await DataBaseManager.FromDatabaseAsync("PromartDesktop", configuration.ConnectionStrings.Default, destionationFile);

            this.IsEnabled = true;

            if (result == null)
                MessageBox.Show("Backup realizado com sucesso!", "Backup realizado", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                Error.ShowDatabaseError($"Ocorreu um erro ao tentar realizar o backup do banco de dados.", result);
        }

        private void RestoreBackup_Click(object sender, RoutedEventArgs e)
        {
            var backupSelect = new BackupSelectWindow();
            backupSelect.ShowDialog();
        }

        private void OpenBackupDirectory_Click(object sender, RoutedEventArgs e)
        {
            var destination = System.IO.Path.Combine(Environment.CurrentDirectory, "Backups");

            Process.Start("explorer.exe", destination);
        }

        private async void MigrateOldDb_Click(object sender, RoutedEventArgs e)
        {
            var config = ConfigManager.Open();
            var result = await DataBaseManager.MigrateOldDatabaseAsync(config.ConnectionStrings.Default);

            if (result != null)
                Error.ShowDatabaseError($"Ocorreu um erro ao tentar migrar os dados.", result);
            else
                MessageBox.Show("Migração realizada com sucesso.", "Migração", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
