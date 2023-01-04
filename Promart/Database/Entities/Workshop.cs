using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Promart.Database.Entities
{
    public class Workshop
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string? Name { get; set; }
        [StringLength(500)]
        public string? Description { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public Workshop()
        {
            Students = new HashSet<Student>();
        }
    }
}
