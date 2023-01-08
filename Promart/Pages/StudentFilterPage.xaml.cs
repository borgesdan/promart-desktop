using Microsoft.EntityFrameworkCore;
using Promart.Core;
using Promart.Database;
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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Reflection;

namespace Promart.Pages
{
    /// <summary>
    /// Interaction logic for StudentFilterPage.xaml
    /// </summary>
    public partial class StudentFilterPage : Page
    {
        public StudentFilterPage()
        {
            InitializeComponent();

            Loaded += StudentFilterPage_Loaded;
        }

        private async void StudentFilterPage_Loaded(object sender, RoutedEventArgs e)
        {
            using var context = App.AppDbContext;

            var students = await context.Students.ToListAsync();

            DataGridResult.ItemsSource = students.Select(s => new StudentFilter
            {
                FullName = s.FullName,
                Gender = s.Gender.Description(),
                ResponsibleName = s.ResponsibleName,
                ResponsiblePhone = s.ResponsiblePhone,
                Registry = s.Registry,
                RegistryDate = s.RegistryDate?.ToShortDateString()
            });
        }

        public class StudentFilter
        {
            [DisplayName("Nome")]
            public string? FullName { get; set; }
            [DisplayName("Sexo")]
            public string? Gender { get; set; }
            [DisplayName("Responsável")]
            public string? ResponsibleName { get; set; }
            [DisplayName("Telefone")]
            public string? ResponsiblePhone { get; set; }
            [DisplayName("Matrícula")]
            public string? Registry { get; set; }
            [DisplayName("Data Matrícula")]
            public string? RegistryDate { get; set; }
        }

        private void DataGridResult_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            var property = (PropertyDescriptor)e.PropertyDescriptor;
            var displayName = property?.Attributes[typeof(DisplayNameAttribute)] as DisplayNameAttribute;

            if (displayName != null && !string.IsNullOrEmpty(displayName.DisplayName))
            {
                e.Column.Header = displayName.DisplayName;
            }
        }
    }
}
