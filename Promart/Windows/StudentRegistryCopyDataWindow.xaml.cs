using Microsoft.EntityFrameworkCore;
using Promart.Core;
using Promart.Database.Entities;
using Promart.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace Promart.Windows
{
    /// <summary>
    /// Interaction logic for StudentRegistryCopyDataWindow.xaml
    /// </summary>
    public partial class StudentRegistryCopyDataWindow : Window
    {
        public class CopyDataResult
        {
            public Student? Student { get; set; }
            public StudentRegistryCopyDataItemsWindow.SelectedItems? SelectedFields { get; set; }
        }

        private CopyDataResult? _result;

        public StudentRegistryCopyDataWindow()
        {
            InitializeComponent();
        }

        public CopyDataResult? GetResult() => _result;

        private void DataGridResult_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            var property = (PropertyDescriptor)e.PropertyDescriptor;
            var displayName = property?.Attributes[typeof(DisplayNameAttribute)] as DisplayNameAttribute;

            if (displayName != null && !string.IsNullOrEmpty(displayName.DisplayName))
            {
                e.Column.Header = displayName.DisplayName;
            }
            else
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }

        private async void Search_Click(object sender, RoutedEventArgs e)
        {
            DataGridResult.ItemsSource = null;

            if (string.IsNullOrWhiteSpace(FullName.Text))
                return;            

            using var context = App.AppDbContext;
            var students = context.Students.Where(s => s.FullName != null && s.FullName.Contains(FullName.Text)).AsQueryable();

            var result = await students.ToListAsync();
            
            DataGridResult.ItemsSource = result.Select(s => new StudentFilter(s));
        }

        private void DataGridResult_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            SelectStudent.IsEnabled = DataGridResult.SelectedItem != null;
        }

        private void SelectStudent_Click(object sender, RoutedEventArgs e)
        {
            var itemsWindow = new StudentRegistryCopyDataItemsWindow();
            var dialogResult = itemsWindow.ShowDialog();

            if(dialogResult != true)
            {
                DialogResult = false;
                Close();
                return;
            }

            _result = new CopyDataResult
            {
                Student = ((StudentFilter)DataGridResult.SelectedItem).Student,
                SelectedFields = itemsWindow.GetResult(),
            };

            DialogResult = true;
            Close();
        }
    }
}
