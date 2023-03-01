using Promart.Core;
using Promart.Database.Entities;
using System.Windows.Controls;
using System.Windows.Media;

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
            ProjectShift.Content = _student.ProjectShift.Description();
            RegistryDate.Content = _student.ProjectRegistryDate?.ToShortDateString();
            Status.Content = _student.ProjectStatus.Description().ToUpper();

            var status = _student.ProjectStatus;

            switch (status)
            {
                case Database.ProjectStatusType.Matriculado:
                    Status.Background = Brushes.Green;
                    break;
                case Database.ProjectStatusType.Aprovado:
                    Status.Background = Brushes.DeepSkyBlue;
                    break;
                case Database.ProjectStatusType.Desistente:
                    Status.Background = Brushes.Red;
                    break;
                case Database.ProjectStatusType.Desaprovado:
                    Status.Background = Brushes.IndianRed;
                    break;
                case Database.ProjectStatusType.Espera:
                    Status.Background = Brushes.DarkSlateBlue;
                    break;
                case Database.ProjectStatusType.ExAluno:
                    Status.Background = Brushes.DarkGray;
                    break;
                case Database.ProjectStatusType.Inscrito:
                    Status.Background = Brushes.BlueViolet;
                    break;
                default:
                    Status.Background = Brushes.DarkOrange;
                    break;
            }
        }

        public Student? GetStudent() => _student;
    }
}
