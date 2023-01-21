using Promart.Core;
using Promart.Database.Entities;
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

namespace Promart.Pages.Print
{
    /// <summary>
    /// Interaction logic for StudentRegisterPrintPage.xaml
    /// </summary>
    public partial class StudentRegisterPrintPage : Page
    {
        public Grid GetMainControl() => MainGrid;

        public StudentRegisterPrintPage(Student student)
        {
            InitializeComponent();

            FullName.Text = student.FullName;
            BirthDate.Text = student.BirthDate?.ToShortDateString();
            Gender.Text = student.Gender != Database.GenderType.Indefinido ? student.Gender.Description().First().ToString() : null;
            RG.Text = student.RG;
            CPF.Text = student.CPF;
            ResponsibleName.Text = student.ResponsibleName;
            ResponsiblePhone.Text = student.ResponsiblePhone;
            Relationship.Text = student.Relationship.Description();
            Dwelling.Text = student.Dwelling.Description();
            Income.Text = student.MonthlyIncome.Description();
            IsBeneficiay.Text = student.IsGovernmentBeneficiary == true ? "Sim" : "Não";
            Street.Text = student.AddressStreet;
            District.Text = student.AddressDistrict;
            Number.Text = student.AddressNumber;
            Complement.Text = student.AddressComplement;
            Reference.Text = student.AddressReferencePoint;
            School.Text = student.SchoolName;
            SchoolYear.Text = student.SchoolYear.Description();
            SchoolShift.Text = student.SchoolShift.Description();
            ProjectShift.Text = student.ProjectShift.Description();
            ProjectStatus.Text = student.ProjectStatus.Description();
            Observations.Text = student.Observations;

            var builder = new StringBuilder();
            student.Workshops.ToList().ForEach(w =>
            {
                builder.Append(w.Name).Append(";").Append(" ");
            });

            Workshops.Text = builder.ToString();
        }
    }
}
