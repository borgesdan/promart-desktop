using Promart.Core;
using Promart.Database;
using Promart.Database.Entities;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Promart.Controls
{
    /// <summary>
    /// Interaction logic for WorkshopDataControl.xaml
    /// </summary>
    public partial class WorkshopDataControl : UserControl
    {
        static int brushCount = 0;

        private Workshop _workshop;
        
        private Brush[] _brushes = new Brush[]
        {
            Brushes.AliceBlue,
            Brushes.LightGray,
        };


        public WorkshopDataControl(Workshop workshop)
        {
            InitializeComponent();

            _workshop = workshop;            

            MainGrid.Background = _brushes[brushCount % 2];
            brushCount++;
        }

        public Workshop GetWorkshop() => new Workshop
        {
            Id = _workshop.Id,
            Name = FullName.Text.Trim(),
            Description = Description.Text.Trim(),
            RegistryStatus = (RegistryStatus)RegistryStatus.SelectedIndex,
        };

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FullName.Text = _workshop.Name;
            Description.Text = _workshop.Description;
            RegistryStatus.SelectedIndex = _workshop.RegistryStatus != Database.RegistryStatus.Deleted 
                ? (int)_workshop.RegistryStatus : (int)Database.RegistryStatus.Inactive;
        }
    }
}
