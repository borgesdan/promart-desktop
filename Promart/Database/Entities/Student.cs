using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Promart.Database.Entities
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string? FullName { get; set; }
        
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
        public string? AddressStreet { get; set; }
        [StringLength(50)]
        public string? AddressDistrict { get; set; }
        [StringLength(10)]
        public string? AddressNumber { get; set; }
        [StringLength(50)]
        public string? AddressComplement { get; set; }
        [StringLength(50)]
        public string? AddressCity { get; set; }
        [StringLength(20)]
        public string? AddressState { get; set; }
        [StringLength(10)]
        public string? AddressCEP { get; set; }
        [StringLength(200)]
        public string? AddressReferencePoint { get; set; }

        [StringLength(500)]
        public string? SchoolName { get; set; }
        public SchoolYearType SchoolYear { get; set; }
        public SchoolShiftType SchoolShift { get; set; }

        public ProjectStatusType ProjectStatus { get; set; }
        public SchoolShiftType ProjectShift { get; set; }
        [StringLength(20)]
        public string? ProjectRegistry { get; set; }
        public DateTime? ProjectRegistryDate { get; set; }

        [StringLength(5000)]
        public string? Observations { get; set; }

        [StringLength(500)]
        public string? PhotoUrl { get; set; }
        [StringLength(100)]
        public string? PhotoTransform { get; set; } //x=50;y=50;sx=2;sy=2

        [Required]
        public RegistryStatus RegistryStatus { get; set; } = RegistryStatus.Active;

        public virtual ICollection<Workshop> Workshops { get; set; } = new HashSet<Workshop>();
        public virtual ICollection<FamilyRelationship> FamilyRelationships { get; set; } = new HashSet<FamilyRelationship>();

        public Student()
        {
            Gender = GenderType.Indefinido;
            Relationship = StudentRelationshipType.Indefinido;
            Dwelling = DwellingType.Indefinido;
            MonthlyIncome = MonthlyIncomeType.Indefinido;
            SchoolYear = SchoolYearType.Indefinido;
            SchoolShift = SchoolShiftType.Indefinido;
        }

        public Student(Student student) : this()
        {
            Id = student.Id;
            FullName = student.FullName;
            BirthDate = student.BirthDate;
            Gender = student.Gender;
            RG = student.RG;
            CPF = student.CPF;
            Certidao = student.Certidao;
            ResponsibleName = student.ResponsibleName;
            ResponsiblePhone = student.ResponsiblePhone;
            Relationship = student.Relationship;
            IsGovernmentBeneficiary = student.IsGovernmentBeneficiary;
            Dwelling = student.Dwelling;
            MonthlyIncome = student.MonthlyIncome;
            AddressStreet = student.AddressStreet;
            AddressState = student.AddressState;
            AddressReferencePoint = student.AddressReferencePoint;
            AddressNumber = student.AddressNumber;
            AddressDistrict = student.AddressDistrict;
            AddressComplement = student.AddressComplement;
            AddressCity = student.AddressCity;
            AddressCEP = student.AddressCEP;
            SchoolName = student.SchoolName;
            SchoolShift = student.SchoolShift;
            SchoolYear = student.SchoolYear;
            ProjectRegistry = student.ProjectRegistry;
            ProjectRegistryDate = student.ProjectRegistryDate;
            ProjectShift = student.ProjectShift;
            ProjectStatus = student.ProjectStatus;
            Observations = student.Observations;
            RegistryStatus = student.RegistryStatus;
            Workshops = student.Workshops.Select(s => new Workshop(s)).ToList();
            FamilyRelationships = student.FamilyRelationships.Select(f => new FamilyRelationship(f)).ToList();
        }
    }
}
