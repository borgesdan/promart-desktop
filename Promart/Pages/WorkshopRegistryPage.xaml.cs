using Microsoft.EntityFrameworkCore;
using Promart.Controls;
using Promart.Core;
using Promart.Database;
using Promart.Database.Context;
using Promart.Database.Entities;
using Promart.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Promart.Pages
{
    /// <summary>
    /// Interaction logic for WorkshopRegistryPage.xaml
    /// </summary>
    public partial class WorkshopRegistryPage : Page
    {
        readonly MainWindowService _mainWindowService = new MainWindowService();

        int _workshopId;
        Workshop? _workshop;
        AppDbContext _context = AppDbContextFactory.Create();
        bool isUpdateMode = false;
        int _page = 1;
        int _pageCount = 30;
        int _total = -1;

        public WorkshopRegistryPage() 
        {
            InitializeComponent();            

            Update.Visibility = Visibility.Collapsed;
            StudentsCountGroup.Visibility = Visibility.Collapsed;
            StudentsRegisteredCountGroup.Visibility = Visibility.Collapsed;
            StudentList.Visibility = Visibility.Collapsed;
            SituationGroup.Visibility = Visibility.Collapsed;
            PageManagerPanel.Visibility = Visibility.Collapsed;
        }

        public WorkshopRegistryPage(int workshopId)
        {
            InitializeComponent();
            _workshopId = workshopId;
            isUpdateMode = true;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CurrentStudentsList.Children.Clear();
            Situation.AddEnum<RegistryStatus>();
            Situation.Items.RemoveAt(2);

            if (!isUpdateMode)
                return;

            Register.Visibility = Visibility.Collapsed;
            Update.Visibility = Visibility.Visible;
            StudentsCountGroup.Visibility = Visibility.Visible;
            StudentsRegisteredCountGroup.Visibility = Visibility.Visible;
            StudentList.Visibility = Visibility.Visible;
            SituationGroup.Visibility = Visibility.Visible;
            PageManagerPanel.Visibility = Visibility.Visible;

            _workshop = _context.Workshops
                .Include(w => w.Students)   
                .FirstOrDefault(w => w.Id == _workshopId);

            this.IsEnabled = _workshop != null;

            if (_workshop == null)
                return;

            FullName.Text = _workshop.Name;
            Description.Text = _workshop.Description;
            Situation.SelectedIndex = _workshop.RegistryStatus != RegistryStatus.Deleted ? (int)_workshop.RegistryStatus : (int)RegistryStatus.Inactive;
            StudentsCount.Text = _workshop.Students.Count.ToString();
            StudentsRegistredCount.Text = _workshop.Students
                .Where(s => s.ProjectStatus == Database.ProjectStatusType.Matriculado)
                .Count()
                .ToString();

            SearchStudents();            
        }

        void SearchStudents()
        {
            PageManagerPanel.IsEnabled = true;
            Next.IsEnabled = true;
            Preview.IsEnabled = true;
            CurrentStudentsList.Children.Clear();

            var pageCountContent = ((ComboBoxItem)PageCount.SelectedItem).Content;
            _pageCount = int.Parse((string)pageCountContent);

            List<Student> students = null;
            int count = 0;

            if (OnlyRegistered.IsChecked == true)
            {
                students = _workshop.Students
                    .Where(s => s.ProjectStatus == Database.ProjectStatusType.Matriculado)
                    .OrderBy(s => s.FullName)
                    .Skip((_page - 1) * _pageCount)
                    .Take(_pageCount)
                    .ToList();

                count = _workshop.Students.Where(s => s.ProjectStatus == Database.ProjectStatusType.Matriculado).Count();
            }
            else
            {
                students = _workshop.Students
                    .OrderBy(s => s.FullName)
                    .Skip((_page - 1) * _pageCount)
                    .Take(_pageCount)
                    .ToList();

                count = _workshop.Students.Count();
            }

            if (students.Count > 0)
            {
                _total = count;   

                Total.Text = _total.ToString();
                PageNumber.Text = _page.ToString();

                if ((_page * _pageCount) >= _total)
                {
                    Next.IsEnabled = false;
                }

                if (_page == 1)
                {
                    Preview.IsEnabled = false;
                }

                students.ForEach(s =>
                {
                    var control = new StudentDetailControl(s);
                    control.MouseLeftButtonDown += (o, e) =>
                    {
                        _mainWindowService.NavigateToStudentRegistryPage(s.Id, s.FullName);
                    };

                    CurrentStudentsList.Children.Add(control);
                });
            }
            else
            {
                _total = 0;
                _page = 1;                
                PageManagerPanel.IsEnabled = false;
            }
        }

        private bool Validate()
        {
            MainGrid.TrimAllTextBox();

            if (string.IsNullOrWhiteSpace(FullName.Text))
            {
                MessageBox.Show(
                    "O nome da oficina é um dado obrigatório. Digite o nome da oficina antes de continuar.",
                    "Nome da oficina",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                    );
                FullName.Focus();
                return false;
            }

            return true;
        }

        private async void Register_Click(object sender, RoutedEventArgs e)
        {
            if (!Validate())
                return;

            _workshop = new Workshop
            {
                Name = FullName.Text,
                Description = Description.Text,
                RegistryStatus = (RegistryStatus)Situation.GetEnum<RegistryStatus>(),
            };

            try
            {
                _context.Add(_workshop);
                await _context.SaveChangesAsync();

                MessageBox.Show("Oficina criada com sucesso", "Oficina Criada", MessageBoxButton.OK, MessageBoxImage.Information);

                isUpdateMode = true;
                _mainWindowService.NavigateToWorkshopListPage(System.Windows.Navigation.NavigationUIVisibility.Hidden);
            }
            catch(Exception ex)
            {
                Error.ShowDatabaseError("Ocorreu um erro ao criar a oficina", ex);
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (_workshop == null)
                return;

            try
            {
                _workshop.Name = FullName.Text;
                _workshop.Description = Description.Text;
                _workshop.RegistryStatus = (RegistryStatus)Situation.GetEnum<RegistryStatus>();
                
                _context.SaveChanges();
                MessageBox.Show("Oficina atualizada com sucesso", "Oficina Atualizada", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Error.ShowDatabaseError("Ocorreu um erro ao atualizar a oficina", ex);
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if ((_page * _pageCount) < _total)
            {
                _page++;
                SearchStudents();
            }
        }

        private void Preview_Click(object sender, RoutedEventArgs e)
        {
            if (_page > 1)
            {
                _page--;
                SearchStudents();
            }
        }

        private void PageCount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!IsLoaded)
                return;

            _page = 1;
            SearchStudents();
        }

        private void OnlyRegistered_Click(object sender, RoutedEventArgs e)
        {
            _page = 1;
            SearchStudents();
        }
    }
}
