using Microsoft.EntityFrameworkCore;
using Promart.Controls;
using Promart.Database.Entities;
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
            using var context = App.AppDbContext;

            var students = context.Students.AsQueryable();

            if (!string.IsNullOrWhiteSpace(FullName.Text))
                students = students.Where(s => s.FullName != null && s.FullName.Contains(FullName.Text));

            string registryAge = string.IsNullOrWhiteSpace(RegistryAge.Text) ? DateTime.Now.Year.ToString() : RegistryAge.Text;

            students = students.Where(s => s.ProjectRegistryDate != null && s.ProjectRegistryDate.Value.Year.ToString() == registryAge);

            return await students.ToListAsync();
        }

        private async void Search_Click(object sender, RoutedEventArgs e)
        {
            ResultPanel.Children.Clear();

            var result = await GetStudents();

            if (result.Count > 0)
                result.ForEach(s => {
                    var control = new StudentDetailControl(s);
                    
                    control.MouseLeftButtonDown += (s, e) => {
                        MainWindow.Instance.OpenNewStudentRegisterTab(control.GetStudent());
                    };

                    ResultPanel.Children.Add(control);
                });
            else
                MessageBox.Show("Não foi encontrado nenhum resultado para essa busca.", "Rematricula não aplicada", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RegistryAge.Text = DateTime.Now.Year.ToString();
        }
    }
}
