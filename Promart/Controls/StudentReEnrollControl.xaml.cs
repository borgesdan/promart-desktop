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

namespace Promart.Controls
{
    /// <summary>
    /// Interaction logic for StudentReEnrollControl.xaml
    /// </summary>
    public partial class StudentReEnrollControl : UserControl
    {
        private readonly Student _student;

        public StudentReEnrollControl(Student student)
        {
            InitializeComponent();

            _student = student;

            FullName.Content = _student.FullName;
            RegistryDate.Content = _student.RegistryDate?.ToShortDateString();
        }

        public bool IsSelectedReEnroll() => Action.SelectedIndex == 1;
        public Student GetStudent() => _student;

        private void Action_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckAction.IsChecked = Action.SelectedIndex == 1;
        }
    }
}
