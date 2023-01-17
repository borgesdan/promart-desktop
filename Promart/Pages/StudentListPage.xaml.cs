using Microsoft.EntityFrameworkCore;
using Promart.Controls;
using Promart.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Promart.Pages
{
    /// <summary>
    /// Interaction logic for StudentListPage.xaml
    /// </summary>
    public partial class StudentListPage : Page
    {
        public StudentListPage()
        {
            InitializeComponent();
        }

        private async Task<List<Student>> GetStudents()
        {
            var context = App.AppDbContext;

            var students = context.Students.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(FullName.Text))
                students = students.Where(s => s.FullName != null && s.FullName.Contains(FullName.Text));

            string registryAge = string.IsNullOrWhiteSpace(RegistryAge.Text) ? DateTime.Now.Year.ToString() : RegistryAge.Text;

            var result = await students
                .Where(s => s.ProjectRegistryDate != null && s.ProjectRegistryDate.Value.Year.ToString() == registryAge)
                .Select(s => new Student
                {
                    Id = s.Id,
                    FullName = s.FullName,
                    BirthDate = s.BirthDate,
                    ProjectRegistry = s.ProjectRegistry,
                    ProjectRegistryDate = s.ProjectRegistryDate,
                    ProjectStatus = s.ProjectStatus
                })
                .ToListAsync();

            return result;
        }

        private async void Search_Click(object sender, RoutedEventArgs e)
        {
            ResultPanel.Children.Clear();

            var result = await GetStudents();

            if (result.Count > 0)
                result.ForEach(s => {
                    var control = new StudentDetailControl(s);

                    control.MouseLeftButtonDown += Control_MouseLeftButtonDown;

                    ResultPanel.Children.Add(control);
                });
            else
                MessageBox.Show("Não foi encontrado nenhum resultado para essa busca.", "Rematricula não aplicada", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Control_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var controlStudent = ((StudentDetailControl)sender).GetStudent();

            if (controlStudent == null)
                return;

            var context = App.AppDbContext;
            var student = context.Students
                .Where(s => s.Id == controlStudent.Id)
                .Include(s => s.Workshops)
                .Include(s => s.FamilyRelationships)                
                .SingleOrDefault();

            if(student == null) 
                return;

            MainWindow.Instance.NavigateToStudentRegisterPage(student);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RegistryAge.Text = DateTime.Now.Year.ToString();
        }
    }
}
