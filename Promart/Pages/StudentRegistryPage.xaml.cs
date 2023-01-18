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

        Student? _student;

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
                ProjectRegistry = RegistryGenerator.NewRegistry(FullName.Text),
                ProjectRegistryDate = DateTime.Now,
                Observations = Observations.Text,
            };

            SetStudentWorkshops(student);
            SetStudentRelationships(student);

            try
            {
                var context = App.AppDbContext;

                context.Update(student);
                await context.SaveChangesAsync();

                MessageBox.Show(
                $"Aluno matrículado com sucesso. Matrícula: {student.ProjectRegistry}",
                "Aluno matriculado",
                MessageBoxButton.OK,
                MessageBoxImage.Information
                );

                IsEnabled = false;
                MainWindow.Instance.NavigateToStudentRegisterPage(student, NavigationUIVisibility.Hidden);
            }
            catch (Exception ex)
            {
                Error.ShowMessageBox("Ocorreu um erro ao matricular o aluno.", ex);
            }
        }

        private async void Update_Click(object sender, RoutedEventArgs e)
        {
            if (_student == null || !Validate())
                return;
            try
            {
                var context = App.AppDbContext;
                _student = context.Students.Where(s => s.Id == _student.Id).Single();

                _student.FullName = FullName.Text;
                _student.BirthDate = BirthDate.SelectedDate;
                _student.Gender = (GenderType)Gender.SelectedIndex;
                _student.CPF = CPF.Text;
                _student.RG = RG.Text;
                _student.Certidao = Certidao.Text;
                _student.ResponsibleName = Responsible.Text;
                _student.ResponsiblePhone = Phone.Text;
                _student.Relationship = (StudentRelationshipType)FamilyRelationship.SelectedIndex;
                _student.IsGovernmentBeneficiary = BeneficiaryBox.SelectedIndex == 0;
                _student.Dwelling = (DwellingType)Dwelling.SelectedIndex;
                _student.MonthlyIncome = (MonthlyIncomeType)MonthlyIncome.SelectedIndex;
                _student.AddressStreet = Address.Text;
                _student.AddressDistrict = AddressDistrict.Text;
                _student.AddressNumber = AddressNumber.Text;
                _student.AddressComplement = AddressComplement.Text;
                _student.AddressCity = "Ipiaú";
                _student.AddressState = "BA";
                _student.AddressCEP = "45570000";
                _student.AddressReferencePoint = AddressReference.Text;
                _student.SchoolName = SchoolName.Text;
                _student.SchoolYear = (SchoolYearType)SchoolYear.SelectedIndex;
                _student.SchoolShift = (SchoolShiftType)SchoolShift.SelectedIndex;

                if (_student.ProjectStatus != ProjectStatusType.Matriculado
                    && (ProjectStatusType)ProjectStatus.SelectedIndex == ProjectStatusType.Matriculado)
                {
                    _student.ProjectRegistryDate = DateTime.Now;
                    RegistryDate.Text = _student.ProjectRegistryDate?.ToShortDateString();
                }

                _student.ProjectStatus = (ProjectStatusType)ProjectStatus.SelectedIndex;
                _student.ProjectShift = (SchoolShiftType)ProjectShift.SelectedIndex;
                _student.Observations = Observations.Text;

                SetStudentWorkshops(_student);
                SetStudentRelationships(_student);

                await context.SaveChangesAsync();

                MessageBox.Show(
                    $"Cadastro atualizado com sucesso",
                    "Cadastro atualizado",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                    );

                RegistryNumber.Focus();
            }
            catch(Exception ex )
            {
                MessageBox.Show(
                "Ocorreu um erro ao atualizar o cadastro do aluno. " +
                "O banco de dados pode estar desconectado ou não foi possível acessar o banco de dados.\n\n\n" +
                $"Erro completo:{ex.Message}",
                "Ocorreu um erro ao matricular o aluno",
                MessageBoxButton.OK,
                MessageBoxImage.Error
                );
            }            
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
                SetRelationships(student);

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

                SetWorkshops(student);
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
            Update.Visibility = Visibility.Visible;

            RegistryNumber.Text = _student.ProjectRegistry;
            RegistryDate.Text = _student.ProjectRegistryDate?.ToShortDateString();

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

            SetRelationships(_student);
            SetWorkshops(_student);
        }

        private void SetRelationships(Student student)
        {
            student.FamilyRelationships?.ToList().ForEach(r =>
                RelationshipPanel.Children.Add(new StudentRelationshipControl(r)));
        }

        private void SetWorkshops(Student student)
        {
            foreach (var workshop in Workshops.Items)
            {
                var checkBox = (CheckBox)workshop;
                var content = (Workshop)checkBox.Content;

                if (student.Workshops != null && student.Workshops.Any(w => w.Id == content.Id))
                    checkBox.IsChecked = true;
            }
        }

        private void SetStudentWorkshops(Student student)
        {
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

            student.Workshops.Clear();
            student.Workshops = workshops;
        }

        private void SetStudentRelationships(Student student)
        {
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

            student.FamilyRelationships = relationships;
        }        

        private async Task GetllWorkshopsAsync()
        {
            try
            {
                var context = App.AppDbContext;
                var workshops = await context.Workshops.ToListAsync();

                workshops.ForEach(w => Workshops.Items.Add(new CheckBox()
                {
                    Content = w,
                    VerticalContentAlignment = VerticalAlignment.Center
                }));
            }
            catch (Exception ex)
            {
                Error.ShowMessageBox("Não foi possível carregar as oficinas do projeto.", ex);
            }
        }        
    }
}