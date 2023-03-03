using Promart.Controls;
using Promart.Pages;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace Promart.Services
{
    public class MainWindowService
    {
        public static MainWindow? MainWindowInstance { get; private set; }
        
        public double ContentPageMinWidth { get; set; } = 800;
        public double ContentPageMaxWidth { get; set; } = 1280;

        private readonly MainWindow _mainWindow;
        private readonly TabControl _mainTab;

        public MainWindowService(MainWindow? window)
        {
            if(window == null)
                throw new ArgumentNullException(nameof(window));

            MainWindowInstance ??= window;

            _mainWindow = window;
            _mainTab = _mainWindow.MainTab;
        }

        public MainWindowService() : this(MainWindowInstance)
        {            
        }

        public TabItem CreateNewTab(string header, Page contentPage)
        {
            contentPage.MinWidth = ContentPageMinWidth;
            contentPage.MaxWidth = ContentPageMaxWidth;
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

        public void NavigateTo(string? header, Page contentPage, NavigationUIVisibility navigationUIVisibility = NavigationUIVisibility.Visible)
        {
            contentPage.MinWidth = ContentPageMinWidth;
            contentPage.MaxWidth = ContentPageMaxWidth;
            contentPage.Title = header;

            var tabItem = (TabItem)_mainWindow.MainTab.SelectedItem;
            tabItem.Header = new HeaderControl(header);

            var scrollViewer = (ScrollViewer)_mainWindow.MainTab.SelectedContent;
            scrollViewer.ScrollToHome();
            var frame = (Frame)scrollViewer.Content;

            frame.NavigationUIVisibility = navigationUIVisibility;
            frame.Navigate(contentPage);
        }

        public void OpenStudentRegistryTab()
        {
            var tabItem = CreateNewTab("Registro de Aluno", new StudentRegistryPage());
            _mainTab.Items.Add(tabItem);
        }

        public void OpenWorkshopRegistryTab()
        {
            var tabItem = CreateNewTab("Nova Oficina", new WorkshopRegistryPage());
            _mainTab.Items.Add(tabItem);
        }

        public void OpenStudentFilterTab()
        {
            var tabItem = CreateNewTab("Filtro de Aluno", new StudentFilterPage());
            _mainTab.Items.Add(tabItem);
        }

        public void OpenWorkshopList()
        {
            var tabItem = CreateNewTab("Lista de Oficinas", new WorkshopListPage());
            _mainTab.Items.Add(tabItem);
        }

        public void OpenStudentListTab()
        {
            var tabItem = CreateNewTab("Lista de Alunos", new StudentListPage());
            _mainTab.Items.Add(tabItem);
        }

        public void NavigateToStudentRegistryPage(int studentId, string? studentName, NavigationUIVisibility navigationUIVisibility = NavigationUIVisibility.Visible)
        {
            var studentPage = new StudentRegistryPage(studentId);
            var header = studentName;

            NavigateTo(header, studentPage, navigationUIVisibility);
        }

        public void NavigateToWorkshopRegistryPage(int workshopId, string? workshopName, NavigationUIVisibility navigationUIVisibility = NavigationUIVisibility.Visible)
        {
            var workshopPage = new WorkshopRegistryPage(workshopId);
            var header = workshopName;

            NavigateTo(header, workshopPage, navigationUIVisibility);
        }

        public void NavigateToWorkshopRegistryPage(NavigationUIVisibility navigationUIVisibility = NavigationUIVisibility.Visible)
        {
            var workshopPage = new WorkshopRegistryPage();

            NavigateTo("Cadastro de Oficina", workshopPage, navigationUIVisibility);
        }

        public void NavigateToWorkshopListPage(NavigationUIVisibility navigationUIVisibility = NavigationUIVisibility.Visible)
        {
            var workshopPage = new WorkshopListPage();

            NavigateTo("Lista de Oficinas", workshopPage, navigationUIVisibility);
        }

        public void Exit()
        {
            var result = MessageBox.Show("Deseja encerrar o programa?", "Encerrar", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
                return;

            _mainWindow.Close();            
        }
    }
}
