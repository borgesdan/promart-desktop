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
        private FamilyRelationship? _relationship;

        public bool IsFormularyValid => !string.IsNullOrWhiteSpace(FullName.Text);        

        public StudentRelationshipControl(FamilyRelationship? relationship)
        {
            InitializeComponent();            

            _relationship = relationship;
            Relationship.AddEnum<FamilyRelationshipType>();

            if (_relationship != null)
            {
                FullName.Text = _relationship.FullName;
                Income.Text = _relationship.Income;
                Occupation.Text = _relationship.Occupation;
                Schooling.Text = _relationship.Schooling;
                Relationship.SelectedIndex = (int)_relationship.Relationship;
                Age.Text = _relationship.Age.ToString();
            }            
        }

        private void FormularyChanged()
        {
            if (_relationship == null)
                return;

            MainGrid.TrimAllTextBox();

            _relationship.FullName = FullName.Text;
            _relationship.Income = Income.Text;
            _relationship.Occupation = Occupation.Text;
            _relationship.Schooling = Schooling.Text;
            _relationship.Relationship = Relationship.GetEnum<FamilyRelationshipType>() ?? FamilyRelationshipType.Indefinido;

            bool parse = int.TryParse(Age.Text, out int age);

            if (parse)
                _relationship.Age = age;
        }

        public FamilyRelationship? GetRelationship() => _relationship;

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            FullName.TextChanged += (object sender, TextChangedEventArgs e) => FormularyChanged();
            Age.TextChanged += (object sender, TextChangedEventArgs e) => FormularyChanged();
            Income.TextChanged += (object sender, TextChangedEventArgs e) => FormularyChanged();
            Occupation.TextChanged += (object sender, TextChangedEventArgs e) => FormularyChanged();
            Schooling.TextChanged += (object sender, TextChangedEventArgs e) => FormularyChanged();
            Relationship.SelectionChanged += (object sender, SelectionChangedEventArgs e) => FormularyChanged();
        }

        private void Remove_Click(object sender, System.Windows.RoutedEventArgs e)
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
