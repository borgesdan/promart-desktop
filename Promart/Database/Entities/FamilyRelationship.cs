using System.ComponentModel.DataAnnotations;
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

        public Student? Student { get; set; }

        public string GetFullString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(FullName);
            builder.Append(" ,");

            builder.Append(Relationship.Description());
            builder.Append(" ,");

            if (Age != null)
            {
                builder.Append(Age);
                builder.Append(" ,");
            }

            if(Occupation != null)
            {
                builder.Append(Occupation);
                builder.Append(" ,");
            }

            if(Schooling != null)
            {
                builder.Append(Schooling);
                builder.Append(" ,");
            }

            if(Income != null)
            {
                builder.Append(Income);                
            }

            return builder.ToString();
        }

        public override string ToString()
        {
            return FullName ?? string.Empty;
        }
    }
}
