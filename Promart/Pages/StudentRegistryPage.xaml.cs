using Promart.Core;
using Promart.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using Microsoft.EntityFrameworkCore;

namespace Promart.Pages
{
    /// <summary>
    /// Interaction logic for StudentRegistryPage.xaml
    /// </summary>
    public partial class StudentRegistryPage : Page
    {
        bool isLoaded = false;

        public StudentRegistryPage()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (isLoaded)
                return;

            Gender.AddEnum<GenderType>();
            FamilyRelationship.AddEnum<StudentRelationshipType>();
            Dwelling.AddEnum<DwellingType>();
            MonthlyIncome.AddEnum<MonthlyIncomeType>();
            SchoolShift.AddEnum<SchoolShiftType>();
            SchoolYear.AddEnum<SchoolYearType>();
            ProjectStatus.AddEnum<ProjectStatusType>();
            ProjectShift.AddEnum<SchoolShiftType>();

            var workshops = await App.AppDbContext.Workshops.ToListAsync();
            workshops.ForEach(w => Workshops.Items.Add(new CheckBox() 
                { 
                    Content = w,
                })
            );

            isLoaded = true;
        }
    }
}