using Promart.Core;
using Promart.Database.Entities;
using System.Windows.Controls;

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
            Age.Content = _student.BirthDate?.GetAge().ToString();
            Registry.Content = _student.ProjectRegistry;
            RegistryDate.Content = _student.ProjectRegistryDate?.ToShortDateString();
            Status.Content = _student.ProjectStatus.Description();
        }

        public Student? GetStudent() => _student;
    }
}
