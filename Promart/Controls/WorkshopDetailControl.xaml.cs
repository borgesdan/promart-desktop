using Promart.Core;
using Promart.Database.Entities;
using System.Windows.Controls;
using System.Windows.Media;
using static Promart.Pages.WorkshopListPage;

namespace Promart.Controls
{
    /// <summary>
    /// Interaction logic for WorkshopDetailControl.xaml
    /// </summary>
    public partial class WorkshopDetailControl : UserControl
    {
        private Workshop _workshop;

        public WorkshopDetailControl(WorkshopDetailData workshop)
        {
            InitializeComponent();

            _workshop = workshop.Workshop;

            FullName.Content = _workshop.Name;
            Status.Content = _workshop.RegistryStatus.Description().ToUpper();
            StudentCount.Content = workshop.StudentCount;
            RegisteredStudentsCount.Content = workshop.RegisteredStudentsCount;
            ToolTip = string.IsNullOrWhiteSpace(_workshop.Description) ? "Insira uma descrição na oficina para parecer aqui" : _workshop.Description;

            var status = _workshop.RegistryStatus;

            switch (status)
            {
                case Database.RegistryStatus.Active:
                    Status.Background = Brushes.Green;
                    break;
                default:
                    Status.Background = Brushes.DarkGray;
                    break;
            }
        }
    }
}
