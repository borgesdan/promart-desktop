using Microsoft.EntityFrameworkCore;
using Promart.Controls;
using Promart.Core;
using Promart.Database;
using Promart.Database.Context;
using Promart.Database.Entities;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Promart.Pages
{
    /// <summary>
    /// Interaction logic for WorkshopRegistryPage.xaml
    /// </summary>
    public partial class WorkshopRegistryPage : Page
    {
        int _workshopId;
        Workshop? _workshop;
        AppDbContext _context = AppDbContextFactory.Create();
        bool isUpdateMode = false;

        public WorkshopRegistryPage() 
        {
            InitializeComponent();
            Update.Visibility = Visibility.Collapsed;
            StudentsCountGroup.Visibility = Visibility.Collapsed;
            StudentsRegisteredCountGroup.Visibility = Visibility.Collapsed;
            StudentList.Visibility = Visibility.Collapsed;
            SituationGroup.Visibility = Visibility.Collapsed;            
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


            _workshop.Students
                .Where(s => s.ProjectStatus == Database.ProjectStatusType.Matriculado)
                .ToList()
                .ForEach(s =>
                {
                    var control = new StudentDetailControl(s);
                    control.MouseLeftButtonDown += (o, e) =>
                    {
                        MainWindow.Instance?.NavigateToStudentRegisterPage(s.Id, s.FullName);
                    };

                    CurrentStudentsList.Children.Add(control);
                });
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
                MainWindow.Instance?.NavigateToWorkshopListPage(System.Windows.Navigation.NavigationUIVisibility.Hidden);
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
    }
}
