using Promart.Database.Entities;
using System.Collections.Generic;

namespace Promart.Database.Responses
{
    public class FamilyRelationshipResponse
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public int? Age { get; set; }
        public FamilyRelationshipType Relationship { get; set; } = FamilyRelationshipType.Indefinido;
        public string? Occupation { get; set; }
        public string? Schooling { get; set; }
        public string? Income { get; set; }
        public RegistryStatus RegistryStatus { get; set; } = RegistryStatus.Active;
        public virtual ICollection<Student> Student { get; set; } = new HashSet<Student>();

        public FamilyRelationshipResponse() { }

        public FamilyRelationshipResponse(FamilyRelationship family)
        {
            Id = family.Id;
            FullName = family.FullName;
            Age = family.Age;
            Relationship = family.Relationship;
            Occupation = family.Occupation;
            Schooling = family.Schooling;
            Income = family.Income;
            RegistryStatus = family.RegistryStatus;
            Student = family.Student;
        }

        public FamilyRelationship GetModel()
        {
            return new FamilyRelationship
            {
                Id = this.Id,
                FullName = this.FullName,
                Age = this.Age,
                Relationship = this.Relationship,
                Occupation = this.Occupation,
                Schooling = this.Schooling,
                Income = this.Income,
                RegistryStatus = this.RegistryStatus,
                Student = this.Student,
            };
        }
    }
}
