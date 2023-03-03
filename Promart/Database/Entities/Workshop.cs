using Promart.Database.Responses;
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

        public int? OldId { get; set; }

        public virtual ICollection<Student> Students { get; set; } = new HashSet<Student>();

        public Workshop()
        {
            Students = new List<Student>();
        }

        public Workshop(Workshop workshop) : this()
        {
            Id = workshop.Id;
            Name = workshop.Name;
            Description = workshop.Description;
            RegistryStatus = workshop.RegistryStatus;
            Students = workshop.Students;
        }

        public Workshop(WorkshopResponse workshop)
        {
            Id = workshop.Id;
            Name = workshop.Name;
            Description = workshop.Description;
            RegistryStatus = workshop.RegistryStatus;
            Students = workshop.Students;
        }

        public override string ToString()
        {
            return Name ?? "Nome não informado";
        }
    }
}
