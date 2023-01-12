using Microsoft.EntityFrameworkCore;
using Promart.Controls;
using Promart.Core;
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
    /// Interaction logic for StudenReEnrollPage.xaml
    /// </summary>
    public partial class StudenReEnrollPage : Page
    {
        public StudenReEnrollPage()
        {
            InitializeComponent();

            ProjectYear.ApplyOnlyNumbers();
        }

        private async void Search_Click(object sender, RoutedEventArgs e)
        {
            ResultPanel.Children.Clear();
            Apply.IsEnabled = true;

            using var context = App.AppDbContext;            

            var students = context.Students.AsQueryable();

            if (!string.IsNullOrWhiteSpace(FullName.Text))
                students = students.Where(s => s.FullName != null && s.FullName.Contains(FullName.Text));     
            
            if(!string.IsNullOrWhiteSpace(ProjectYear.Text))
                students = students.Where(s => s.RegistryDate != null && s.RegistryDate.Value.Year.ToString() == ProjectYear.Text);

            var result = await students.ToListAsync();

            if(result.Count > 0)
                result.ForEach(s => ResultPanel.Children.Add(new StudentReEnrollControl(s)));
            else
                Apply.IsEnabled = false;
        }

        private async void Apply_Click(object sender, RoutedEventArgs e)
        {            
            using var context = App.AppDbContext;
            bool hasModification = false;

            foreach (var child in ResultPanel.Children)
            {
                var control = (StudentReEnrollControl)child;

                if (control.IsSelectedReEnroll())
                {
                    var student = control.GetStudent();
                    student.RegistryDate = DateTime.Now;

                    context.Update(student);

                    hasModification = true;
                }
            }

            if (hasModification)
            {
                await context.SaveChangesAsync();

                MessageBox.Show("Alunos rematriculados com sucesso.", "Rematricula aplicada", MessageBoxButton.OK, MessageBoxImage.Information);

                ResultPanel.Children.Clear();
                Apply.IsEnabled = false;
            }
            else
            {
                MessageBox.Show("Nenhum dado foi alterado.", "Rematricula não aplicada", MessageBoxButton.OK, MessageBoxImage.Information);
                ResultPanel.Children.Clear();
                Apply.IsEnabled = false;
            }

        }
    }
}
