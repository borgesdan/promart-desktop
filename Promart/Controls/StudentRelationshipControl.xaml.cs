using Promart.Core;
using Promart.Database;
using Promart.Database.Entities;
using System.Windows;
using System.Windows.Controls;

namespace Promart.Controls
{
    /// <summary>
    /// Interaction logic for StudentRelationshipControl.xaml
    /// </summary>
    public partial class StudentRelationshipControl : UserControl
    {
        public bool IsFormularyValid => !string.IsNullOrWhiteSpace(FullName.Text);        

        public StudentRelationshipControl(FamilyRelationship? relationship)
        {
            InitializeComponent();            
            
            Relationship.AddEnum<FamilyRelationshipType>();

            if (relationship != null)
            {
                FullName.Text = relationship.FullName;
                Income.Text = relationship.Income;
                Occupation.Text = relationship.Occupation;
                Schooling.Text = relationship.Schooling;
                Relationship.SelectedIndex = (int)relationship.Relationship;
                Age.Text = relationship.Age.ToString();
            }            
        }        

        public FamilyRelationship? GetRelationship()
        {
            MainGrid.TrimAllTextBox();

            return new FamilyRelationship
            {
                FullName = FullName.Text,
                Income = Income.Text,
                Occupation = Occupation.Text,
                Schooling = Schooling.Text,
                Relationship = Relationship.GetEnum<FamilyRelationshipType>() ?? FamilyRelationshipType.Indefinido,
                Age = int.TryParse(Age.Text, out int age) ? age : null,
            };
        }    

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Deseja realmente remover esse registro?",
                "Remover",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes)
                return;

            var parent = (Panel)Parent;
            parent.Children.Remove(this);
        }
    }
}
