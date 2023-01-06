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
using Promart.Windows;
using Promart.Database.Entities;

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

        private void AddRelationship_Click(object sender, RoutedEventArgs e)
        {
            var relationshipWindow = new FamilyRelationshipWindow();
            var dialogResult = relationshipWindow.ShowDialog();

            if (dialogResult != true)
                return;

            var result = relationshipWindow.GetResult();

            var button = new Button
            {
                Content = result.GetFullString()
            };

            var index = RelationshipPanel.Children.Count;
            RelationshipPanel.Children.Insert(index - 1, button);
        }

        private async void Register_Click(object sender, RoutedEventArgs e)
        {
            var student = new Student
            {
                FullName = FullName.Text,
                BirthDate = BirthDate.SelectedDate,
                Gender = Gender.GetEnum<GenderType>() ?? GenderType.Indefinido,
                CPF = CPF.Text,
                RG = RG.Text,
                Certidao = Certidao.Text,
                ResponsibleName = Responsible.Text,
                ResponsiblePhone = Phone.Text,
                Relationship = FamilyRelationship.GetEnum<StudentRelationshipType>() ?? StudentRelationshipType.Indefinido,
                IsGovernmentBeneficiary = BeneficiaryBox.SelectedIndex == 0,
                Dwelling = Dwelling.GetEnum<DwellingType>() ?? DwellingType.Indefinido,
                MonthlyIncome = MonthlyIncome.GetEnum<MonthlyIncomeType>() ?? MonthlyIncomeType.Indefinido,
                Street = Address.Text,
                District = AddressDistrict.Text,
                Number = AddressNumber.Text,
                Complement = AddressComplement.Text,
                City = "Ipiaú",
                State = "Bahia",
                CEP = "45570000",
                ReferencePoint = AddressReference.Text,
                SchoolName = SchoolName.Text,
                SchoolYear = SchoolYear.GetEnum<SchoolYearType>() ?? SchoolYearType.Indefinido,
                SchoolShift = SchoolShift.GetEnum<SchoolShiftType>() ?? SchoolShiftType.Indefinido,
                Status = ProjectStatus.GetEnum<ProjectStatusType>() ?? ProjectStatusType.Indefinido,
                ProjectShift = ProjectShift.GetEnum<SchoolShiftType>() ?? SchoolShiftType.Indefinido,
                RegistryDate = DateTime.Now,
                Observations = Observations.Text,
            };

            var workshops = new List<Workshop>();
            
            foreach(var item in Workshops.Items)
            {
                var checkbox = (CheckBox)item;

                if(checkbox.IsChecked == true)
                {
                    var content = (Workshop)checkbox.Content;
                    workshops.Add(content);
                }
            }

            student.Workshops = workshops;

            App.AppDbContext.Students.Add(student);
            await App.AppDbContext.SaveChangesAsync();

            Register.IsEnabled = false;
        }
    }
}