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
        public StudentRegisterPrintPage(Student student)
        {
            InitializeComponent();

            FullName.Text = student.FullName;
            BirthDate.Text = student.BirthDate?.ToShortDateString();
            Gender.Text = student.Gender.Description();
            RG.Text = student.RG;
            CPF.Text = student.CPF;
        }
    }
}
