﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using Promart.Core;
using Promart.Database;
using Promart.Database.Context;
using Promart.Filters;
using Promart.Services;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Promart.Pages
{
    /// <summary>
    /// Interaction logic for StudentFilterPage.xaml
    /// </summary>
    public partial class StudentFilterPage : Page
    {
        bool isLoaded = false;
        AppDbContext context = AppDbContextFactory.Create();
        bool contextDisposed = false;
        readonly MainWindowService _mainWindow = new MainWindowService();

        public StudentFilterPage()
        {
            InitializeComponent();

            Loaded += StudentFilterPage_Loaded;
        }

        private async void StudentFilterPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (isLoaded)
                return;

            Age.ApplyOnlyNumbers();
            Gender.AddEnum<GenderType>();
            FamilyRelationship.AddEnum<StudentRelationshipType>();
            Dwelling.AddEnum<DwellingType>();
            MonthlyIncome.AddEnum<MonthlyIncomeType>();
            SchoolShift.AddEnum<SchoolShiftType>();
            SchoolYear.AddEnum<SchoolYearType>();
            ProjectStatus.AddEnum<ProjectStatusType>();
            ProjectShift.AddEnum<SchoolShiftType>();

            await SearchAsync();

            if (ColumnsList.Children.Count == 0 && DataGridResult.Items.Count > 0)
                CreateCheckBoxes();

            Export.IsEnabled = DataGridResult.Items.Count > 0;
        }

        private void CreateCheckBoxes()
        {
            DataGridResult.Columns.ToList().ForEach(c =>
            {
                if (c.Visibility != Visibility.Visible)
                    return;

                var checkbox = new CheckBox
                {
                    Tag = c,
                    Content = c.Header,
                    MinWidth = 180,
                    IsChecked = true
                };

                checkbox.Click += ColumnsCheckbox_Click;
                ColumnsList.Children.Add(checkbox);
            });
        }

        private void ColumnsCheckbox_Click(object sender, RoutedEventArgs e)
        {
            var checkbox = (CheckBox)sender;
            var column = DataGridResult.Columns.SingleOrDefault(c => c.Header == checkbox.Content);            

            if (column!= null)
                column.Visibility = checkbox.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
        }

        private void CheckColumnList(CheckBox checkBox)
        {
            var column = DataGridResult.Columns.SingleOrDefault(c => c.Header == checkBox.Content);

            if (column != null)
                column.Visibility = checkBox.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
        }

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

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            FiltersGrid.ForEachVisual(v =>
            {
                if (v is CheckBox cb)
                {
                    cb.IsChecked = false;
                }
            });
        }

        private async Task SearchAsync()
        {
            MainGrid.TrimAllTextBox();
            
            var students = context.Students.AsQueryable();

            if (!string.IsNullOrWhiteSpace(SearchBar.Text))
            {
                if (RadioName.IsChecked == true)
                    students = students.Where(s => s.FullName != null && s.FullName.Contains(SearchBar.Text));
                else
                    students = students.Where(s => s.CPF != null && s.CPF == SearchBar.Text);
            }

            if (CheckAge.IsChecked == true && !string.IsNullOrWhiteSpace(Age.Text))
            {
                var parse = int.TryParse(Age.Text, out int age);

                if (parse)
                {
                    var nowBirthday = DateTime.Now.AddYears(-age);
                    var limit = new DateTime(nowBirthday.Year - 1, nowBirthday.Month, nowBirthday.Day + 1);

                    if (AgeSelector.SelectedIndex == 0) //Igual a
                    {
                        students = students.Where(s =>
                            s.BirthDate != null
                            && s.BirthDate.Value > limit
                            && s.BirthDate.Value <= nowBirthday);

                    }
                    else if (AgeSelector.SelectedIndex == 1) //Igual ou maior
                    {
                        students = students.Where(s =>
                            s.BirthDate != null
                            && s.BirthDate.Value <= nowBirthday);
                    }
                    else if (AgeSelector.SelectedIndex == 2) //Igual ou menor
                    {
                        students = students.Where(s =>
                            s.BirthDate != null
                            && s.BirthDate.Value > limit);
                    }
                    else if (AgeSelector.SelectedIndex == 3) //Maior que
                    {
                        students = students.Where(s =>
                            s.BirthDate != null
                            && s.BirthDate.Value <= limit);
                    }
                    else if (AgeSelector.SelectedIndex == 4) //Menor que
                    {
                        students = students.Where(s =>
                            s.BirthDate != null
                            && s.BirthDate.Value > nowBirthday);
                    }
                }
            }

            if (CheckGender.IsChecked == true)
            {
                var value = Gender.GetEnum<GenderType>() ?? GenderType.Indefinido;
                students = students.Where(s => s.Gender == value);
            }

            if (CheckFamilyRelationship.IsChecked == true)
            {
                var value = FamilyRelationship.GetEnum<StudentRelationshipType>() ?? StudentRelationshipType.Indefinido;
                students = students.Where(s => s.Relationship == value);
            }

            if (CheckDwelling.IsChecked == true)
            {
                var value = Dwelling.GetEnum<DwellingType>() ?? DwellingType.Indefinido;
                students = students.Where(s => s.Dwelling == value);
            }

            if (CheckMonthlyIncome.IsChecked == true)
            {
                var value = MonthlyIncome.GetEnum<MonthlyIncomeType>() ?? MonthlyIncomeType.Indefinido;
                students = students.Where(s => s.MonthlyIncome == value);
            }

            if (CheckBeneficiaryBox.IsChecked == true)
            {
                students = students.Where(s => s.IsGovernmentBeneficiary == (BeneficiaryBox.SelectedIndex == 0 ? true : false));
            }

            if (CheckAddress.IsChecked == true && !string.IsNullOrWhiteSpace(Address.Text))
            {
                students = students.Where(s => s.AddressStreet != null && s.AddressStreet.Contains(Address.Text));
            }

            if (CheckAddressDistrict.IsChecked == true && !string.IsNullOrWhiteSpace(AddressDistrict.Text))
            {
                students = students.Where(s => s.AddressDistrict != null && s.AddressDistrict.Contains(AddressDistrict.Text));
            }

            if (CheckSchoolName.IsChecked == true && !string.IsNullOrWhiteSpace(SchoolName.Text))
            {
                students = students.Where(s => s.SchoolName != null && s.SchoolName.Contains(SchoolName.Text));
            }

            if (CheckSchoolShift.IsChecked == true)
            {
                var value = SchoolShift.GetEnum<SchoolShiftType>() ?? SchoolShiftType.Indefinido;
                students = students.Where(s => s.SchoolShift == value);
            }

            if (CheckSchoolYear.IsChecked == true)
            {
                var value = SchoolYear.GetEnum<SchoolYearType>() ?? SchoolYearType.Indefinido;
                students = students.Where(s => s.SchoolYear == value);
            }

            if (CheckProjectStatus.IsChecked == true)
            {
                var value = ProjectStatus.GetEnum<ProjectStatusType>() ?? ProjectStatusType.Indefinido;
                students = students.Where(s => s.ProjectStatus == value);
            }

            if (CheckProjectShift.IsChecked == true)
            {
                var value = ProjectShift.GetEnum<SchoolShiftType>() ?? SchoolShiftType.Indefinido;
                students = students.Where(s => s.ProjectShift == value);
            }

            if (CheckBirthMonth.IsChecked == true)
            {
                students = students.Where(s => s.BirthDate != null && s.BirthDate.Value.Month == BirthMonth.SelectedIndex + 1);
            }

            try
            {
                var result = await students.AsNoTracking().ToListAsync();

                DataGridResult.ItemsSource = result.Select(s => new StudentFilter(s));
                TotalFilterRegistry.Content = result.Count;
            }
            catch(Exception ex)
            {
                DataGridResult.ItemsSource = null;
                TotalFilterRegistry.Content = 0;

                Error.ShowDatabaseError("Ocorreu um erro na tentativa de filtrar os dados.", ex);
            }
        }

        private async void Search_Click(object sender, RoutedEventArgs e)
        {
            await SearchAsync();

            Export.IsEnabled = DataGridResult.Items.Count > 0;
            
            if (ColumnsList.Children.Count == 0 && DataGridResult.Items.Count > 0)
                CreateCheckBoxes();

            foreach (var col in ColumnsList.Children)
            {
                var checkbox = col as CheckBox;
                CheckColumnList(checkbox);
            }
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataGridResult.SelectAllCells();
                DataGridResult.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;

                ApplicationCommands.Copy.Execute(null, DataGridResult);
                DataGridResult.UnselectAllCells();

                string result = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
                result = result.Replace(',', ';');

                var saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Arquivo CSV (*.csv)|*.csv";

                if (saveFileDialog.ShowDialog() == true)
                {
                    File.WriteAllText(saveFileDialog.FileName, result, Encoding.UTF8);
                    MessageBox.Show($"Os dados foram exportados com sucesso", "Dados exportados", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro ao tentar exportar os dados.\n\n Erro: {ex.Message}", "Erro ao Exportar", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            contextDisposed = true;
            context.Dispose();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if(contextDisposed)
            {
                context = AppDbContextFactory.Create();
                contextDisposed = false;
            }
        }

        private void DataGridResult_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var datagrid = DataGridResult;

            var selected = datagrid.SelectedValue;

            if (selected == null)
                return;

            var studentFilter = (StudentFilter)selected;
            _mainWindow.NavigateToStudentRegistryPage(studentFilter.GetId(), studentFilter.FullName);
        }
    }
}
