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
using Promart.Core.Html;
using Promart.Controls;
using System.Security.RightsManagement;

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

            Register.Click += async (sender, e) => await Register_Click(sender, e);
            BirthDate.SelectedDateChanged += BirthDate_SelectedDateChanged;

            FullName.ApplyOnlyLetterOrWhiteSpace();
            Responsible.ApplyOnlyLetterOrWhiteSpace();
            CPF.ApplyOnlyNumbers();
            Phone.ApplyOnlyNumbers();
        }

        private void BirthDate_SelectedDateChanged(object? sender, SelectionChangedEventArgs e)
        {
            var now = DateTime.Now;
            var selected = BirthDate.SelectedDate;

            if (selected == null)
                return;

            var diff = now - selected;

            Age.Content = $"{(int)(diff.Value.TotalDays / 365)} anos";
        }

        private void AddRelationship_Click(object sender, RoutedEventArgs e)
        {
            var relationshipWindow = new FamilyRelationshipWindow();
            var dialogResult = relationshipWindow.ShowDialog();

            if (dialogResult != true)
                return;

            var result = relationshipWindow.GetResult();
            var control = new StudentRelationshipControl(result);

            RelationshipPanel.Children.Add(control);
        }

        private bool Validate()
        {
            MainGrid.TrimAllTextBox();

            if (string.IsNullOrWhiteSpace(FullName.Text))
            {
                MessageBox.Show(
                    "O nome do aluno é um dado obrigatório. Digite o nome do aluno antes de continuar.",
                    "Nome do aluno",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                    );
                FullName.Focus();
                return false;
            }

            if (FullName.Text.Length < 3)
            {
                MessageBox.Show(
                   "O nome do aluno deve conter no minímo 3 letras.",
                   "Nome do aluno",
                   MessageBoxButton.OK,
                   MessageBoxImage.Warning
                   );
                FullName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(Responsible.Text))
            {
                MessageBox.Show(
                    "O nome do responsável é um dado obrigatório. Digite o nome do responsável antes de continuar.",
                    "Nome do responsável",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                    );
                FullName.Focus();
                return false;
            }

            if (FullName.Text.Length < 3)
            {
                MessageBox.Show(
                   "O nome do responsável deve conter no minímo 3 letras.",
                   "Nome do responsável",
                   MessageBoxButton.OK,
                   MessageBoxImage.Warning
                   );
                FullName.Focus();
                return false;
            }

            return true;
        }

        private async Task Register_Click(object sender, RoutedEventArgs e)
        {
            if (!Validate())
                return;

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
                AddressStreet = Address.Text,
                AddressDistrict = AddressDistrict.Text,
                AddressNumber = AddressNumber.Text,
                AddressComplement = AddressComplement.Text,
                AddressCity = "Ipiaú",
                AddressState = "BA",
                AddressCEP = "45570000",
                AddressReferencePoint = AddressReference.Text,
                SchoolName = SchoolName.Text,
                SchoolYear = SchoolYear.GetEnum<SchoolYearType>() ?? SchoolYearType.Indefinido,
                SchoolShift = SchoolShift.GetEnum<SchoolShiftType>() ?? SchoolShiftType.Indefinido,
                ProjectStatus = ProjectStatus.GetEnum<ProjectStatusType>() ?? ProjectStatusType.Indefinido,
                ProjectShift = ProjectShift.GetEnum<SchoolShiftType>() ?? SchoolShiftType.Indefinido,
                RegistryDate = DateTime.Now,
                Observations = Observations.Text,
            };

            student.Registry = RegistryGenerator.NewRegistry(student.FullName);

            var workshops = new List<Workshop>();

            foreach (var item in Workshops.Items)
            {
                var checkbox = (CheckBox)item;

                if (checkbox.IsChecked == true)
                {
                    var content = (Workshop)checkbox.Content;
                    workshops.Add(content);
                }
            }

            student.Workshops = workshops;

            var relationships = new List<FamilyRelationship>();

            foreach (var child in RelationshipPanel.Children)
            {
                if (child is StudentRelationshipControl relationshipControl)
                {
                    var relationship = relationshipControl.GetRelationship();

                    if (relationship != null && relationshipControl.IsFormularyValid)
                        relationships.Add(relationship);
                }
            }

            student.Relationships = relationships;

            this.IsEnabled = false;

            try
            {
                using var context = App.AppDbContext;

                context.Update(student);
                await context.SaveChangesAsync();

                MessageBox.Show(
                $"Aluno matrículado com sucesso. Matrícula: {student.Registry}",
                "Aluno matriculado",
                MessageBoxButton.OK,
                MessageBoxImage.Information
                );
            }
            catch(Exception ex ) 
            {
                MessageBox.Show(
                "Ocorreu um erro ao matricular o aluno. Veja possíveis causas:\n\n" +
                "1) O formulário contém dados inválidos.\n" +
                "2) O banco de dados está desconectado ou não foi possível acessar o banco de dados.\n\n\n" +
                $"Erro completo:{ex.Message}",
                "Ocorreu um erro ao matricular o aluno",
                MessageBoxButton.OK,
                MessageBoxImage.Error
                );

                this.IsEnabled = true;
            }
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

            using var context = App.AppDbContext;

            var workshops = await context.Workshops.ToListAsync();
            workshops.ForEach(w => Workshops.Items.Add(new CheckBox()
            {
                Content = w,
                VerticalContentAlignment = VerticalAlignment.Center
            }));

            isLoaded = true;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            StudentRegistryCopyDataWindow dataWindow = new StudentRegistryCopyDataWindow();
            var dialogResult = dataWindow.ShowDialog();

            if (dialogResult != true)
                return;

            var result = dataWindow.GetResult();            

            if (result == null)
                return;

            var student = result.Student;
            var fields = result.SelectedFields;

            if (fields.FamilyData)
            {
                Responsible.Text = student.ResponsibleName;
                Phone.Text = student.ResponsiblePhone;
                FamilyRelationship.SelectedIndex = (int)student.Relationship != 99 ? (int)student.Relationship : FamilyRelationship.Items.Count - 1;
                Dwelling.SelectedIndex = (int)student.Dwelling != 99 ? (int)student.Dwelling : Dwelling.Items.Count - 1;
                MonthlyIncome.SelectedIndex = (int)student.MonthlyIncome != 99 ? (int)student.MonthlyIncome : MonthlyIncome.Items.Count - 1;
                BeneficiaryBox.SelectedIndex = student.IsGovernmentBeneficiary != null ? 0 : 1;
            }

            if (fields.FamilyComposition)
            {
                foreach(var relationship in student.Relationships)
                {                    
                    var control = new StudentRelationshipControl(relationship);

                    RelationshipPanel.Children.Add(control);
                }
            }
            
            if (fields.AddressData)
            {
                Address.Text = student.AddressStreet;
                AddressComplement.Text = student.AddressComplement;
                AddressDistrict.Text = student.AddressDistrict;
                AddressNumber.Text = student.AddressNumber;
                AddressReference.Text = student.AddressReferencePoint;
            }

            if (fields.SchoolData)
            {
                SchoolName.Text = student.SchoolName;
                SchoolShift.SelectedIndex = (int)student.SchoolShift != 99 ? (int)student.SchoolShift : SchoolShift.Items.Count - 1;
                SchoolYear.SelectedIndex = (int)student.SchoolYear != 99 ? (int)student.SchoolYear : SchoolYear.Items.Count - 1;
            }
            
            if (fields.ProjectData)
            {                
                ProjectStatus.SelectedIndex = (int)student.ProjectStatus != 99 ? (int)student.ProjectStatus : ProjectStatus.Items.Count - 1;
                ProjectShift.SelectedItem = (int)student.ProjectShift != 99 ? (int)student.ProjectShift : ProjectShift.Items.Count - 1;
                Observations.Text = student.Observations;
                
                foreach(var workshop in Workshops.Items)
                {
                    var checkBox = (CheckBox)workshop;
                    var content = (Workshop)checkBox.Content;

                    if (student.Workshops.Any(w => w.Id == content.Id))
                        checkBox.IsChecked = true;                    
                }
            }            
        }
    }
}