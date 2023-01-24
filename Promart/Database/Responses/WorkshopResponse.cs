using Promart.Database.Entities;
using System.Collections.Generic;

namespace Promart.Database.Responses
{
    public class WorkshopResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public RegistryStatus RegistryStatus { get; set; } = RegistryStatus.Active;
        public virtual ICollection<Student> Students { get; set; } = new HashSet<Student>();

        public WorkshopResponse() { }

        public WorkshopResponse(Workshop workshop) 
        {
            Id = workshop.Id;
            Name = workshop.Name;
            Description = workshop.Description;
            RegistryStatus = workshop.RegistryStatus;
            Students = workshop.Students;
        }

        public Workshop GetModel()
        {
            return new Workshop
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description,
                RegistryStatus = this.RegistryStatus,
                Students = this.Students,
            };
        }
    }
}