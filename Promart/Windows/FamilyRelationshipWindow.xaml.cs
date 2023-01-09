using Promart.Core;
using Promart.Database;
using Promart.Database.Entities;
using System.Windows;

namespace Promart.Windows
{
    /// <summary>
    /// Interaction logic for FamilyRelationshipWindow.xaml
    /// </summary>
    public partial class FamilyRelationshipWindow : Window
    {
        FamilyRelationship? _result;        

        public FamilyRelationshipWindow()
        {
            InitializeComponent();
        }

        public FamilyRelationship? GetResult() => _result;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Relationship.AddEnum<FamilyRelationshipType>();
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.TrimAllTextBox();

            if (string.IsNullOrWhiteSpace(FullName.Text))
            {
                DialogResult = false;
                Close();
                return;
            }

            _result = new FamilyRelationship
            {                
                FullName = FullName.Text,
                Age = int.TryParse(Age.Text, out int age) ? age : null,
                Schooling = Schooling.Text,
                Income = Income.Text,
                Occupation = Ocupation.Text,
                Relationship = Relationship.GetEnum<FamilyRelationshipType>() ?? FamilyRelationshipType.Indefinido,                
            };            

            DialogResult = true;

            Close();
        }        
    }
}
