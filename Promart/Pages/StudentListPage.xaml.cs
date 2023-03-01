using Microsoft.EntityFrameworkCore;
using Promart.Controls;
using Promart.Core;
using Promart.Database.Context;
using Promart.Database.Entities;
using Promart.Database.Responses;
using Promart.Services;
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
        readonly MainWindowService _mainWindow;

        int _page = 1;
        int _pageCount = 30;
        int _total = -1;

        public StudentListPage()
        {
            InitializeComponent();

            _mainWindow = new MainWindowService();
            RegistryAge.Text = DateTime.Now.Year.ToString();
        }

        private async Task<List<Student>> GetStudentsAsync()
        {
            using var context = AppDbContextFactory.Create();

            var students = context.Students
                                    .AsNoTracking()
                                    .AsQueryable();

            if (!string.IsNullOrWhiteSpace(FullName.Text))
                students = students.Where(s => s.FullName != null && s.FullName.Contains(FullName.Text));

            if (OnlyRegistered.IsChecked == true)
                students = students.Where(s => s.ProjectStatus == Database.ProjectStatusType.Matriculado);

            var result = await students
                .Select(s => new Student
                {
                    Id = s.Id,
                    FullName = s.FullName,
                    BirthDate = s.BirthDate,
                    ProjectRegistry = s.ProjectRegistry,
                    ProjectRegistryDate = s.ProjectRegistryDate,
                    ProjectStatus = s.ProjectStatus
                })
                .OrderBy(s => s.FullName)
                .Skip((_page - 1) * _pageCount)
                .Take(_pageCount)
                .ToListAsync();


            _total = await students.CountAsync();

            return result;
        }

        private async Task SearchAsync(bool showNotFoundMessage = true)
        {
            MainGrid.TrimAllTextBox();
            ResultPanel.Children.Clear();
            PageManagerPanel.IsEnabled = true;
            Next.IsEnabled = true;
            Preview.IsEnabled = true;
            
            var pageCountContent = ((ComboBoxItem)PageCount.SelectedItem).Content;
            _pageCount = int.Parse((string)pageCountContent);

            var result = await GetStudentsAsync();

            if (result.Count > 0)
            {
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

                result.ForEach(s =>
                {
                    var control = new StudentDetailControl(s);
                    control.MouseLeftButtonDown += Control_MouseLeftButtonDown;
                    ResultPanel.Children.Add(control);
                });
            }
            else
            {
                if (showNotFoundMessage)
                    MessageBox.Show(
                        "Não foi encontrado nenhum resultado para essa busca.",
                        "Nada foi encontrado",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);

                PageManagerPanel.IsEnabled = false;
            }
        }

        private async void Search_Click(object sender, RoutedEventArgs e)
        {
            _page = 1;
            await SearchAsync();
        }

        private void Control_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var controlStudent = ((StudentDetailControl)sender).GetStudent();

            if (controlStudent == null)
                return;

            _mainWindow.NavigateToStudentRegistryPage(controlStudent.Id, controlStudent.FullName);
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _page = 1;
            await SearchAsync(false);
        }

        private async void Next_Click(object sender, RoutedEventArgs e)
        {
            if ((_page * _pageCount) < _total)
            {
                _page++;
                await SearchAsync();
            }
        }

        private async void Preview_Click(object sender, RoutedEventArgs e)
        {
            if (_page > 1)
            {
                _page--;
                await SearchAsync();
            }
        }

        private async void PageCount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!IsLoaded)
                return;

            _page = 1;
            await SearchAsync();
        }

        private async void OnlyRegistered_Click(object sender, RoutedEventArgs e)
        {
            _page = 1;
            await SearchAsync();
        }
    }
}
