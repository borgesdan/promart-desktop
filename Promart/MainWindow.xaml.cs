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

namespace Promart
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Instance { get; private set; }        

        public MainWindow()
        {   
            InitializeComponent();
            Instance = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StudentReEnroll.Content = $"Rematrícula {DateTime.Now.Year + 1}";
        }

        private static TabItem CreateNewTab(string header, Page contentPage)
        {
            contentPage.MinWidth = 800;
            contentPage.MaxWidth = 1280;
            contentPage.Title = header;

            var frame = new Frame
            {
                Content = contentPage,
                NavigationUIVisibility = NavigationUIVisibility.Hidden
            };

            var scrollViewer = new ScrollViewer
            {
                Content = frame
            };

            var tabItem = new TabItem
            {
                Header = new HeaderControl(header),
                Content = scrollViewer,
                IsSelected = true,
                Background = Brushes.Gray
            };

            return tabItem;
        }  
        
        private void NavigateTo(string? header, Page contentPage, NavigationUIVisibility navigationUIVisibility = NavigationUIVisibility.Visible)
        {
            contentPage.MinWidth = 800;
            contentPage.MaxWidth = 1280;
            contentPage.Title = header;

            var tabItem = (TabItem)MainTab.SelectedItem;
            tabItem.Header = new HeaderControl(header);

            var scrollViewer = (ScrollViewer)MainTab.SelectedContent;
            var frame = (Frame)scrollViewer.Content;

            frame.NavigationUIVisibility = navigationUIVisibility;
            frame.Navigate(contentPage);
        }

        private void StudentRegister_Click(object sender, RoutedEventArgs e)
        {
            var tabItem = CreateNewTab("Novo Aluno", new StudentRegistryPage());
            MainTab.Items.Add(tabItem);
        }

        private void StudentFilter_Click(object sender, RoutedEventArgs e)
        {
            var tabItem = CreateNewTab("Filtro de Aluno", new StudentFilterPage());
            MainTab.Items.Add(tabItem);
        }

        private void WorkshopList_Click(object sender, RoutedEventArgs e)
        {
            var tabItem = CreateNewTab("Lista de Oficinas", new WorkshopListPage());
            MainTab.Items.Add(tabItem);
        }

        private void StudentReEnroll_Click(object sender, RoutedEventArgs e)
        {
            var tabItem = CreateNewTab("Rematricular", new StudenReEnrollPage());
            MainTab.Items.Add(tabItem);
        }

        private void StudentList_Click(object sender, RoutedEventArgs e)
        {
            var tabItem = CreateNewTab("Lista de Alunos", new StudentListPage());
            MainTab.Items.Add(tabItem);
        }                

        public void NavigateToStudentRegisterPage(int studentId, string? studentName, NavigationUIVisibility navigationUIVisibility = NavigationUIVisibility.Visible)
        {
            var studentPage = new StudentRegistryPage(studentId);
            var header = studentName;

            NavigateTo(header, studentPage, navigationUIVisibility);
        }
    }
}
