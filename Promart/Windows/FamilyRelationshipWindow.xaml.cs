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
        StudentRelationship _result;        

        public FamilyRelationshipWindow()
        {
            InitializeComponent();
        }

        public StudentRelationship? GetResult() => _result;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Relationship.AddEnum<FamilyRelationshipType>();
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            _result = new StudentRelationship
            {                
                FullName = FullName.Text,
                Age = int.Parse(Age.Text),
                Schooling = Schooling.Text,
                Income = Income.Text,
                Occupation = Ocupation.Text,
                Relationship = Relationship.GetEnumInSelectedItem<FamilyRelationshipType>() ?? FamilyRelationshipType.Indefinido,                
            };

            DialogResult = true;

            Close();
        }        
    }
}
