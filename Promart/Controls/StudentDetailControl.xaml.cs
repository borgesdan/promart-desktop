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

namespace Promart.Controls
{
    /// <summary>
    /// Interaction logic for StudentDetailControl.xaml
    /// </summary>
    public partial class StudentDetailControl : UserControl
    {
        private readonly Student? _student;

        public StudentDetailControl(Student student)
        {
            InitializeComponent();

            _student = student;

            FullName.Content = _student.FullName;
            Age.Content = 0;
            Registry.Content = _student.ProjectRegistry;
            RegistryDate.Content = _student.ProjectRegistryDate?.ToShortDateString();
            Status.Content = _student.ProjectStatus.Description();
        }

        public Student? GetStudent() => _student;
    }
}
