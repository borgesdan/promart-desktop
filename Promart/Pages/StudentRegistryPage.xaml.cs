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

        readonly Student? _student;

        public StudentRegistryPage()
        {
            InitializeComponent();         
        }
        
        public StudentRegistryPage(Student student)
        {
            InitializeComponent();        
            _student = student;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (isLoaded)
                return;

            FullName.ApplyOnlyLetterOrWhiteSpace();
            Responsible.ApplyOnlyLetterOrWhiteSpace();
            CPF.ApplyOnlyNumbers();
            Phone.ApplyOnlyNumbers();

            Gender.AddEnum<GenderType>();
            FamilyRelationship.AddEnum<StudentRelationshipType>();
            Dwelling.AddEnum<DwellingType>();
            MonthlyIncome.AddEnum<MonthlyIncomeType>();
            SchoolShift.AddEnum<SchoolShiftType>();
            SchoolYear.AddEnum<SchoolYearType>();
            ProjectStatus.AddEnum<ProjectStatusType>();
            ProjectShift.AddEnum<SchoolShiftType>();

            await GetllWorkshopsAsync();

            if (_student != null)
                ApplyStudentData();

            isLoaded = true;
        }        

        private void BirthDate_SelectedDateChanged(object? sender, SelectionChangedEventArgs e)
        {
            Age.Content = BirthDate.SelectedDate?.GetAge();
        }

        private void AddRelationship_Click(object sender, RoutedEventArgs e)
        {
            var relationshipWindow = new FamilyRelationshipWindow();
            var dialogResult = relationshipWindow.ShowDialog();

            if (dialogResult != true)
                return;

            var result = relationshipWindow.GetResult();

            RelationshipPanel.Children.Add(new StudentRelationshipControl(result));
        }        

        private async void Register_Click(object sender, RoutedEventArgs e)
        {
            if (!Validate())
                return;

            this.IsEnabled = false;

            var student = new Student
            {
                FullName = FullName.Text,
                BirthDate = BirthDate.SelectedDate,
                Gender = (GenderType)Gender.SelectedIndex,
                CPF = CPF.Text,
                RG = RG.Text,
                Certidao = Certidao.Text,
                ResponsibleName = Responsible.Text,
                ResponsiblePhone = Phone.Text,
                Relationship = (StudentRelationshipType)FamilyRelationship.SelectedIndex,
                IsGovernmentBeneficiary = BeneficiaryBox.SelectedIndex == 0,
                Dwelling = (DwellingType)Dwelling.SelectedIndex,
                MonthlyIncome = (MonthlyIncomeType)MonthlyIncome.SelectedIndex,
                AddressStreet = Address.Text,
                AddressDistrict = AddressDistrict.Text,
                AddressNumber = AddressNumber.Text,
                AddressComplement = AddressComplement.Text,
                AddressCity = "Ipiaú",
                AddressState = "BA",
                AddressCEP = "45570000",
                AddressReferencePoint = AddressReference.Text,
                SchoolName = SchoolName.Text,
                SchoolYear = (SchoolYearType)SchoolYear.SelectedIndex,
                SchoolShift = (SchoolShiftType)SchoolShift.SelectedIndex,
                ProjectStatus = (ProjectStatusType)ProjectStatus.SelectedIndex,
                ProjectShift = (SchoolShiftType)ProjectShift.SelectedIndex,
                ProjectRegistryDate = DateTime.Now,
                Observations = Observations.Text,
            };

            student.ProjectRegistry = RegistryGenerator.NewRegistry(student.FullName);

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

            var result = await SaveStudentAsync(student);
            IsEnabled = !result;
        }
        
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            var dataWindow = new StudentRegistryCopyDataWindow();
            var dialogResult = dataWindow.ShowDialog();

            if (dialogResult != true)
                return;

            var result = dataWindow.GetResult();            

            if (result == null)
                return;

            var student = result.Student;
            var fields = result.SelectedFields;

            if (fields == null || student == null)
                return;

            if (fields.FamilyData)
            {
                Responsible.Text = student.ResponsibleName;
                Phone.Text = student.ResponsiblePhone;
                FamilyRelationship.SelectedIndex = (int)student.Relationship;
                Dwelling.SelectedIndex = (int)student.Dwelling;
                MonthlyIncome.SelectedIndex = (int)student.MonthlyIncome;
                BeneficiaryBox.SelectedIndex = student.IsGovernmentBeneficiary != null && student.IsGovernmentBeneficiary == true ? 1 : 0;
            }

            if (fields.FamilyComposition)
                SetStudentRelationships(student);

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
                SchoolShift.SelectedIndex = (int)student.SchoolShift;
                SchoolYear.SelectedIndex = (int)student.SchoolYear;
            }
            
            if (fields.ProjectData)
            {
                ProjectStatus.SelectedIndex = (int)student.ProjectStatus;
                ProjectShift.SelectedItem = (int)student.ProjectShift;
                Observations.Text = student.Observations;
                
                SetStudentWorkshops(student);
            }            
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

        private void ApplyStudentData()
        {
            if (_student == null)
                return;

            RegistryExpander.Visibility = Visibility.Visible;
            Register.Visibility = Visibility.Collapsed;

            RegistryNumber.Text = _student.ProjectRegistry;
            RegistryDate.SelectedDate = _student.ProjectRegistryDate;

            FullName.Text = _student.FullName;
            BirthDate.SelectedDate = _student.BirthDate;
            Gender.SelectedIndex = (int)_student.Gender;
            RG.Text = _student.RG;
            CPF.Text = _student.CPF;
            Certidao.Text = _student.Certidao;

            Responsible.Text = _student.ResponsibleName;
            FamilyRelationship.SelectedIndex = (int)_student.Relationship;
            Dwelling.SelectedIndex = (int)_student.Dwelling;
            MonthlyIncome.SelectedIndex = (int)_student.MonthlyIncome;

            BeneficiaryBox.SelectedIndex = _student.IsGovernmentBeneficiary != null
                && _student.IsGovernmentBeneficiary == true
                ? 1 : 0;
            Phone.Text = _student.ResponsiblePhone;

            Address.Text = _student.AddressStreet;
            AddressDistrict.Text = _student.AddressDistrict;
            AddressNumber.Text = _student.AddressNumber;
            AddressComplement.Text = _student.AddressComplement;
            AddressReference.Text = _student.AddressReferencePoint;

            SchoolName.Text = _student.SchoolName;
            SchoolShift.SelectedIndex = (int)_student.SchoolShift;
            SchoolYear.SelectedIndex = (int)_student.SchoolYear;

            ProjectStatus.SelectedIndex = (int)_student.ProjectStatus;
            ProjectShift.SelectedIndex = (int)_student.ProjectShift;
            Observations.Text = _student.Observations;

            SetStudentRelationships(_student);
            SetStudentWorkshops(_student);
        }

        private void SetStudentRelationships(Student student)
        {
            student.Relationships?.ToList().ForEach(r =>
                RelationshipPanel.Children.Add(new StudentRelationshipControl(r)));
        }

        private void SetStudentWorkshops(Student student)
        {
            foreach (var workshop in Workshops.Items)
            {
                var checkBox = (CheckBox)workshop;
                var content = (Workshop)checkBox.Content;

                if (student.Workshops != null && student.Workshops.Any(w => w.Id == content.Id))
                    checkBox.IsChecked = true;
            }
        }        

        private static async Task<bool> SaveStudentAsync(Student student)
        {
            try
            {
                using var context = App.AppDbContext;

                context.Update(student);
                await context.SaveChangesAsync();

                MessageBox.Show(
                $"Aluno matrículado com sucesso. Matrícula: {student.ProjectRegistry}",
                "Aluno matriculado",
                MessageBoxButton.OK,
                MessageBoxImage.Information
                );

                return true;
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

                return false;
            }
        }

        private async Task GetllWorkshopsAsync()
        {
            using var context = App.AppDbContext;
            var workshops = await context.Workshops.ToListAsync();

            workshops.ForEach(w => Workshops.Items.Add(new CheckBox()
            {
                Content = w,
                VerticalContentAlignment = VerticalAlignment.Center
            }));
        }
    }
}