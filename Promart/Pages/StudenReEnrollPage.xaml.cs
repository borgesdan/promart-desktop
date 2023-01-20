using Microsoft.EntityFrameworkCore;
using Promart.Controls;
using Promart.Core;
using Promart.Database.Context;
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
    /// Interaction logic for StudenReEnrollPage.xaml
    /// </summary>
    public partial class StudenReEnrollPage : Page
    {
        public static int nextYear = DateTime.Now.Year + 1;

        public StudenReEnrollPage()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            nextYear = DateTime.Now.Year + 1;
            Observation.Text = $"Nesta página você pode rematricular os alunos deste ano {nextYear - 1} para o ano de {nextYear}.\n" +
                "\n1) Digite o nome do aluno e/ou clique no botão PESQUISAR e uma lista de alunos aparecerá. " +
                "\n2) No lado esquerdo, em AÇÂO, selecione REMATRICULAR. Repita o processo para todos os alunos desejados. " +
                $"\n3) Ao fim, clique em APLICAR. Os alunos serão rematriculados para o ano de {nextYear}";

            await SearchAsync();
        }

        private async Task<List<Student>> GetStudents()
        {
            using var context = AppDbContextFactory.Create();

            var students = context.Students.AsQueryable();

            if (!string.IsNullOrWhiteSpace(FullName.Text))
                students = students.Where(s => s.FullName != null && s.FullName.Contains(FullName.Text));

            students = students.Where(s => s.ProjectRegistryDate != null && s.ProjectRegistryDate.Value.Year.ToString() == DateTime.Now.Year.ToString());

            return await students.ToListAsync();
        }

        private async Task SearchAsync()
        {
            ResultPanel.Children.Clear();

            var result = await GetStudents();

            if (result.Count > 0)
            {
                result.ForEach(s => ResultPanel.Children.Add(new StudentReEnrollControl(s)));
                Apply.IsEnabled = true;
            }
            else
            {
                Apply.IsEnabled = false;
                MessageBox.Show("Não foi encontrado nenhum resultado para essa busca.", "Nada encontrado", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private async void Search_Click(object sender, RoutedEventArgs e)
        {
            await SearchAsync();       
        }

        private async void Apply_Click(object sender, RoutedEventArgs e)
        {
            using var context = AppDbContextFactory.Create();
            bool hasModification = false;

            foreach (var child in ResultPanel.Children)
            {
                var control = (StudentReEnrollControl)child;

                if (control.IsSelectedReEnroll())
                {
                    var student = control.GetStudent();

                    var now = DateTime.Now;
                    student.ProjectRegistryDate = new DateTime(now.Year + 1, 1, 1);
                    student.ProjectStatus = Database.ProjectStatusType.Matriculado;

                    context.Update(student);

                    hasModification = true;
                }
                else
                {
                    var student = control.GetStudent();

                    if(student.ProjectStatus == Database.ProjectStatusType.Matriculado)
                        student.ProjectStatus = Database.ProjectStatusType.ExAluno;

                    context.Update(student);
                }
            }

            if (hasModification)
            {
                var messageBoxResult = MessageBox.Show(
                $"Deseja realmente rematricular os alunos selecionados para o ano de {nextYear}?",
                $"Rematricula {nextYear}",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    await context.SaveChangesAsync();

                    MessageBox.Show(
                        $"Alunos rematriculados para o ano de {nextYear} com sucesso.",
                        $"Rematricula {nextYear} aplicada",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);

                    ResultPanel.Children.Clear();
                    Apply.IsEnabled = false;                    

                    return;
                }                
            }

            MessageBox.Show("Nenhum dado foi alterado.", "Rematricula não aplicada", MessageBoxButton.OK, MessageBoxImage.Information);
            ResultPanel.Children.Clear();
            Apply.IsEnabled = false;
        }       
    }
}
