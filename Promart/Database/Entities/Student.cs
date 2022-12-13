using System;
using System.Collections.Generic;

namespace Promart.Database.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        public GenderType Gender { get; set; }

        public string? CPF { get; set; }
        public string? RG { get; set; }
        public string? Certidao { get; set; }

        public string? ResponsibleName { get; set; }
        public string? ResponsiblePhone { get; set; }

        public StudentRelationshipType Relationship { get; set; }
        public bool? IsGovernmentBeneficiary { get; set; }
        public DwellingType Dwelling { get; set; }
        public MonthlyIncomeType MonthlyIncome { get; set; }

        public string? Street { get; set; }
        public string? District { get; set; }
        public string? Number { get; set; }
        public string? Complement { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? CEP { get; set; }
        public string? ReferencePoint { get; set; }

        public string? SchoolName { get; set; }
        public SchoolYearType SchoolYear { get; set; }
        public SchoolShiftType SchoolShift { get; set; }

        public StudentProjectStatusType Status { get; set; }
        public SchoolShiftType ProjectShift { get; set; }
        public string? Registry { get; set; }
        public DateTime? RegistryDate { get; set; }

        public string? Observations { get; set; }

        public virtual ICollection<Workshop>? Workshops { get; set; }
        public virtual ICollection<StudentRelationship>? Relationships { get; set; }

        public Student()
        {
            Gender = GenderType.Indefinido;
            Relationship = StudentRelationshipType.Indefinido;
            Dwelling = DwellingType.Indefinido;
            MonthlyIncome = MonthlyIncomeType.Indefinido;
            SchoolYear = SchoolYearType.Indefinido;
            SchoolShift = SchoolShiftType.Indefinido;

            Workshops = new HashSet<Workshop>();
            Relationships = new HashSet<StudentRelationship>();
        }
    }
}
