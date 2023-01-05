using System.ComponentModel.DataAnnotations;

namespace Promart.Database.Entities
{
    public class StudentRelationship
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(500)]
        public string? FullName { get; set; }
        
        public int? Age { get; set; }
        
        public FamilyRelationshipType? Relationship { get; set; }

        [StringLength(500)]
        public string? Occupation { get; set; }

        [StringLength(500)] 
        public string? Schooling { get; set; }

        [StringLength(500)] 
        public string? Income { get; set; }

        public Student? Student { get; set; }
    }
}
