using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Promart.Core;

namespace Promart.Database.Entities
{
    public class FamilyRelationship
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string? FullName { get; set; }

        public int? Age { get; set; }

        public FamilyRelationshipType Relationship { get; set; } = FamilyRelationshipType.Indefinido;

        [StringLength(500)]
        public string? Occupation { get; set; }

        [StringLength(500)]
        public string? Schooling { get; set; }

        [StringLength(500)]
        public string? Income { get; set; }

        [Required]
        public RegistryStatus RegistryStatus { get; set; } = RegistryStatus.Active;

        public virtual ICollection<Student> Student { get; set; } = new HashSet<Student>();

        public FamilyRelationship() { }

        public FamilyRelationship(FamilyRelationship relationship) : this()
        {
            Id = relationship.Id;
            FullName = relationship.FullName;
            Age = relationship.Age;
            Relationship = relationship.Relationship;
            Occupation = relationship.Occupation;
            Schooling = relationship.Schooling;
            Income = relationship.Income;
            RegistryStatus = relationship.RegistryStatus;
            //Student = relationship.Student.Select(s => new Student(s)).ToList();
        }

        public string GetFullString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(FullName);
            builder.Append(", ");

            builder.Append(Relationship.Description());
            builder.Append(", ");

            if (Age != null)
            {
                builder.Append(Age);
                builder.Append(", ");
            }

            if(!string.IsNullOrWhiteSpace(Occupation))
            {
                builder.Append(Occupation);
                builder.Append(", ");
            }

            if(!string.IsNullOrWhiteSpace(Schooling))
            {
                builder.Append(Schooling);
                builder.Append(", ");
            }

            if(!string.IsNullOrWhiteSpace(Income))
            {
                builder.Append(Income);                
            }

            builder.Append(";");

            return builder.ToString();
        }

        public override string ToString()
        {
            return FullName ?? string.Empty;
        }
    }
}
