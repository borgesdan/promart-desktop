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
using Promart.Filters;
using System.Linq.Expressions;
using Promart.Database.Entities;

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
            Search.Click += async (sender, e) => await Search_Click(sender, e);
        }

        private async Task Search_Click(object sender, RoutedEventArgs e)
        {
            using var context = App.AppDbContext;

            IQueryable<Student> students = students = context.Students.AsQueryable();

            if (!string.IsNullOrWhiteSpace(FullName.Text))
                students = students.Where(s => s.FullName != null && s.FullName.Contains(FullName.Text));

            if (CheckAge.IsChecked == true && !string.IsNullOrWhiteSpace(Age.Text))
            {
                var parse = int.TryParse(Age.Text, out int age);

                if (parse)
                {
                    var now = DateTime.Now;
                    var one = new DateTime(now.Year, 1, 1);
                    var span = now - one;
                    var days = span.Days;

                    var ageDays = (age * 365) + days;

                    var niver = new DateTime(1989, 10, 05);

                    var value = niver.AddYears(age).AddDays(days);

                    students = students.Where(s => s.BirthDate != null && s.BirthDate.Value.AddDays(ageDays) == DateTime.Now);
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
                ResponsibleName = s.ResponsibleName,
                ResponsiblePhone = s.ResponsiblePhone,
                Registry = s.Registry,
                RegistryDate = s.RegistryDate?.ToShortDateString()
            });
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
    }
}
