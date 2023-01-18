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
            Status.Content = _workshop.RegistryStatus.Description();
            StudentCount.Content = workshop.StudentCount;
            RegisteredStudentsCount.Content = workshop.RegisteredStudentsCount;
        }
    }
}
