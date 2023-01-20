using Promart.Database.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promart.Database.Responses
{
    public class StudentResponse
    {
        Student? _student;

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
        public string? AddressStreet { get; set; }
        public string? AddressDistrict { get; set; }
        public string? AddressNumber { get; set; }
        public string? AddressComplement { get; set; }
        public string? AddressCity { get; set; }
        public string? AddressState { get; set; }
        public string? AddressCEP { get; set; }
        public string? AddressReferencePoint { get; set; }
        public string? SchoolName { get; set; }
        public SchoolYearType SchoolYear { get; set; }
        public SchoolShiftType SchoolShift { get; set; }
        public ProjectStatusType ProjectStatus { get; set; }
        public SchoolShiftType ProjectShift { get; set; }
        public string? ProjectRegistry { get; set; }
        public DateTime? ProjectRegistryDate { get; set; }
        public string? Observations { get; set; }
        public RegistryStatus RegistryStatus { get; set; } = RegistryStatus.Active;

        public virtual ICollection<Workshop> Workshops { get; set; } = new HashSet<Workshop>();
        public virtual ICollection<FamilyRelationship> FamilyRelationships { get; set; } = new HashSet<FamilyRelationship>();

        public StudentResponse() { }

        public StudentResponse(Student student)
        {
            _student = new Student(student);

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

        public Student GetStudent()
        {
            if (_student == null)
                return new Student();

            return _student;
        }
    }
}