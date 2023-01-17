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

        [Required]
        public RegistryStatus RegistryStatus { get; set; } = RegistryStatus.Active;

        public virtual ICollection<Student> Students { get; set; } = new HashSet<Student>();

        public Workshop()
        {
            Students = new List<Student>();
        }

        public override string ToString()
        {
            return Name ?? "Nome não informado";
        }
    }
}
