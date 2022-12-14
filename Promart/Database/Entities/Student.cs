using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Promart.Database.Entities
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string? FullName { get; set; }

        [Required]
        public DateTime? BirthDate { get; set; }

        [Required]
        public GenderType Gender { get; set; }

        [StringLength(14)]
        public string? CPF { get; set; }

        [StringLength(20)] 
        public string? RG { get; set; }

        [StringLength(50)]
        public string? Certidao { get; set; }
        
        [Required]
        [StringLength(500)]
        public string? ResponsibleName { get; set; }

        [StringLength(12)]
        public string? ResponsiblePhone { get; set; }

        public StudentRelationshipType Relationship { get; set; }
        public bool? IsGovernmentBeneficiary { get; set; }
        public DwellingType Dwelling { get; set; }
        public MonthlyIncomeType MonthlyIncome { get; set; }

        [StringLength(200)]
        public string? Street { get; set; }
        [StringLength(50)]
        public string? District { get; set; }
        [StringLength(10)]
        public string? Number { get; set; }
        [StringLength(50)]
        public string? Complement { get; set; }
        [StringLength(50)]
        public string? City { get; set; }
        [StringLength(20)]
        public string? State { get; set; }
        [StringLength(10)]
        public string? CEP { get; set; }
        [StringLength(200)]
        public string? ReferencePoint { get; set; }

        [StringLength(500)]
        public string? SchoolName { get; set; }
        public SchoolYearType SchoolYear { get; set; }
        public SchoolShiftType SchoolShift { get; set; }

        public ProjectStatusType Status { get; set; }
        public SchoolShiftType ProjectShift { get; set; }
        [StringLength(20)]
        public string? Registry { get; set; }
        public DateTime? RegistryDate { get; set; }

        [StringLength(3000)]
        public string? Observations { get; set; }

        public virtual ICollection<Workshop>? Workshops { get; set; }
        public virtual ICollection<FamilyRelationship>? Relationships { get; set; }

        public Student()
        {
            Gender = GenderType.Indefinido;
            Relationship = StudentRelationshipType.Indefinido;
            Dwelling = DwellingType.Indefinido;
            MonthlyIncome = MonthlyIncomeType.Indefinido;
            SchoolYear = SchoolYearType.Indefinido;
            SchoolShift = SchoolShiftType.Indefinido;

            Workshops = new HashSet<Workshop>();
            Relationships = new HashSet<FamilyRelationship>();
        }
    }
}
