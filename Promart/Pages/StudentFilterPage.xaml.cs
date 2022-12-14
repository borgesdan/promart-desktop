using Microsoft.EntityFrameworkCore;
using Promart.Core;
using Promart.Database;
using Promart.Filters;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
            Search.Click += async (sender, e) => Search_Click(sender, e);
        }        

        private void StudentFilterPage_Loaded(object sender, RoutedEventArgs e)
        {
            Gender.AddEnum<GenderType>();
            FamilyRelationship.AddEnum<StudentRelationshipType>();
            Dwelling.AddEnum<DwellingType>();
            MonthlyIncome.AddEnum<MonthlyIncomeType>();
            SchoolShift.AddEnum<SchoolShiftType>();
            SchoolYear.AddEnum<SchoolYearType>();
            ProjectStatus.AddEnum<ProjectStatusType>();
            ProjectShift.AddEnum<SchoolShiftType>();
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

        private async void Search_Click(object sender, RoutedEventArgs e)
        {
            using var context = App.AppDbContext;
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
                var value = Gender.GetEnum<StudentRelationshipType>() ?? StudentRelationshipType.Indefinido;
                students = students.Where(s => s.Relationship == value);
            }

            if (CheckDwelling.IsChecked == true)
            {
                var value = Gender.GetEnum<DwellingType>() ?? DwellingType.Indefinido;
                students = students.Where(s => s.Dwelling == value);
            }

            if (CheckMonthlyIncome.IsChecked == true)
            {
                var value = Gender.GetEnum<MonthlyIncomeType>() ?? MonthlyIncomeType.Indefinido;
                students = students.Where(s => s.MonthlyIncome == value);
            }

            if (CheckBeneficiaryBox.IsChecked == true)
            {
                students = students.Where(s => s.IsGovernmentBeneficiary == (BeneficiaryBox.SelectedIndex == 0 ? true : false));
            }

            if (CheckAddress.IsChecked == true && !string.IsNullOrWhiteSpace(Address.Text))
            {
                students = students.Where(s => s.Street != null && s.Street.Contains(Address.Text));
            }

            if (CheckAddressDistrict.IsChecked == true && !string.IsNullOrWhiteSpace(AddressDistrict.Text))
            {
                students = students.Where(s => s.District != null && s.District.Contains(AddressDistrict.Text));
            }

            if (CheckSchoolName.IsChecked == true && !string.IsNullOrWhiteSpace(SchoolName.Text))
            {
                students = students.Where(s => s.SchoolName != null && s.SchoolName.Contains(SchoolName.Text));
            }

            if (CheckSchoolShift.IsChecked == true)
            {
                var value = Gender.GetEnum<SchoolShiftType>() ?? SchoolShiftType.Indefinido;
                students = students.Where(s => s.SchoolShift == value);
            }

            if (CheckSchoolYear.IsChecked == true)
            {
                var value = Gender.GetEnum<SchoolYearType>() ?? SchoolYearType.Indefinido;
                students = students.Where(s => s.SchoolYear == value);
            }

            if (CheckProjectStatus.IsChecked == true)
            {
                var value = Gender.GetEnum<ProjectStatusType>() ?? ProjectStatusType.Indefinido;
                students = students.Where(s => s.Status == value);
            }

            if (CheckProjectShift.IsChecked == true)
            {
                var value = Gender.GetEnum<SchoolShiftType>() ?? SchoolShiftType.Indefinido;
                students = students.Where(s => s.ProjectShift == value);
            }

            var result = await students.ToListAsync();

            DataGridResult.ItemsSource = result.Select(s => new StudentFilter
            {
                FullName = s.FullName,
                Gender = s.Gender.Description(),
                Age = s.BirthDate != null ? ((int)((DateTime.Now - s.BirthDate.Value).TotalDays / 365)).ToString() : null,
                ResponsibleName = s.ResponsibleName,
                ResponsiblePhone = s.ResponsiblePhone,
                Registry = s.Registry,
                RegistryDate = s.RegistryDate?.ToShortDateString()
            });
        }
    }
}
