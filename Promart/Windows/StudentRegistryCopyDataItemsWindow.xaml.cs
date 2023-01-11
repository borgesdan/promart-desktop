using System.Windows;

namespace Promart.Windows
{
    /// <summary>
    /// Interaction logic for StudentRegistryCopyDataItemsWindow.xaml
    /// </summary>
    public partial class StudentRegistryCopyDataItemsWindow : Window
    {
        public class SelectedItems
        {
            public bool FamilyComposition { get; set; } = false;
            public bool FamilyData { get; set; } = false;
            public bool AddressData { get; set; } = false;
            public bool SchoolData { get; set; } = false;
            public bool ProjectData { get; set; } = false;
        }

        private readonly SelectedItems _selectedItems = new SelectedItems();

        public SelectedItems GetResult()
        {
            return _selectedItems;
        }

        public StudentRegistryCopyDataItemsWindow()
        {
            InitializeComponent();
        }

        private void SelectData_Click(object sender, RoutedEventArgs e)
        {
            _selectedItems.FamilyComposition = FamilyComposition.IsChecked == true;
            _selectedItems.FamilyData = FamilyData.IsChecked == true;
            _selectedItems.AddressData = Address.IsChecked == true;
            _selectedItems.SchoolData = School.IsChecked == true;
            _selectedItems.ProjectData = Project.IsChecked == true;

            DialogResult = true;

            Close();
        }
    }
}
