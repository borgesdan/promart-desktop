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

namespace Promart
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {   
            InitializeComponent();

            StudentRegister.Click += async (sender, e) => StudentRegister_Click(sender, e);
            StudentFilter.Click += async (sender, e) => StudentFilter_Click(sender, e);
        }

        private static TabItem CreateNewTab(object header, Page contentPage)
        {
            contentPage.MinWidth = 800;
            contentPage.MaxWidth = 1280;

            var frame = new Frame
            {
                Content = contentPage,
                NavigationUIVisibility = NavigationUIVisibility.Hidden
            };

            var scrollViewer = new ScrollViewer();
            scrollViewer.Content = frame;

            var tabItem = new TabItem();
            tabItem.Header = header;
            tabItem.Content = scrollViewer;
            tabItem.IsSelected = true;
            return tabItem;
        }

        private async void StudentRegister_Click(object sender, RoutedEventArgs e)
        {
            var tabItem = CreateNewTab("Novo Estudante", new StudentRegistryPage());
            MainTab.Items.Add(tabItem);
        }

        private async void StudentFilter_Click(object sender, RoutedEventArgs e)
        {
            var tabItem = CreateNewTab("Filtro de Aluno", new StudentFilterPage());
            MainTab.Items.Add(tabItem);
        }
    }
}
