using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Promart.Database.Entities
{
    public class ProjectClass
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(4)]
        public string? Year { get; set; }

        public bool IsCurrentClass { get; set; } = false;

        public virtual ICollection<Student>? Students { get; set; }
    }
}