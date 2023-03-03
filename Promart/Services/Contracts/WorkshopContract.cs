using Promart.Database;
using Promart.Database.Entities;
using System.Collections.Generic;

namespace Promart.Services.Contracts
{
    public class WorkshopContract : ServiceContract<Workshop>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public RegistryStatus RegistryStatus { get; set; } = RegistryStatus.Active;
        public ICollection<Student> Students { get; set; } = new HashSet<Student>();
        public int? OldId { get; set; }

        public WorkshopContract(Workshop workshop)
        {
            Id = workshop.Id;
            Name = workshop.Name;
            Description = workshop.Description;
            RegistryStatus = workshop.RegistryStatus;
            Students = workshop.Students;
            OldId = workshop.OldId;
        }

        public override Workshop GetEntity()
        {
            return new Workshop()
            {
                Id = Id,
                Name = Name,
                Description = Description,
                RegistryStatus = RegistryStatus,
                Students = Students,
                OldId = OldId,
            };
        }
    }
}
