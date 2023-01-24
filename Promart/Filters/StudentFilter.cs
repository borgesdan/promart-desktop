using Promart.Core;
using Promart.Database;
using Promart.Database.Entities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Promart.Filters
{
    public class StudentFilter
    {
        [DisplayName("Nome")]
        public string? FullName { get; set; }
        [DisplayName("Sexo")]
        public string? Gender { get; set; }
        [DisplayName("Idade")]
        public string? Age { get; set; }
        [DisplayName("Aniversário")]
        public string? BirthDate { get; set; }
        [DisplayName("Situação")]
        public string? Status { get; set; }
        [DisplayName("Matrícula")]
        public string? Registry { get; set; }
        [DisplayName("Data Matrícula")]
        public string? RegistryDate { get; set; }
        [DisplayName("Turno no Projeto")]
        public string? ProjectShift { get; set; }
        [DisplayName("Nome do Responsável")]
        public string? ResponsibleName { get; set; }
        [DisplayName("Endereço")]
        public string? AddressStreet { get; set; }
        [DisplayName("Bairro")]
        public string? AddressDistrict { get; set; }
        [DisplayName("Número")]
        public string? AddressNumber { get; set; }
        [DisplayName("Complemento")]
        public string? AddressComplement { get; set; }
        [DisplayName("Ponto de Referência")]
        public string? AddressReferencePoint { get; set; }
        [DisplayName("Nome da Escola")]
        public string? SchoolName { get; set; }
        [DisplayName("Ano escolar")]
        public string? SchoolYear { get; set; }
        [DisplayName("Turno Escolar")]
        public string? SchoolShift { get; set; }

        public Student? Student { get; set; }

        public StudentFilter() { }

        public StudentFilter(Student student)
        {
            FullName = student.FullName;
            Gender = student.Gender.Description();
            Age = student.BirthDate != null ? ((int)((DateTime.Now - student.BirthDate.Value).TotalDays / 365)).ToString() : null;
            BirthDate = student.BirthDate?.ToShortDateString();
            Status = student.ProjectStatus.Description();                        
            Registry = student.ProjectRegistry;
            RegistryDate = student.ProjectRegistryDate?.ToShortDateString();
            ProjectShift = student.ProjectShift.Description();
            ResponsibleName = student.ResponsibleName;
            AddressComplement = student.AddressComplement;
            AddressDistrict = student.AddressDistrict;
            AddressNumber = student.AddressNumber;
            AddressReferencePoint = student.AddressReferencePoint;
            SchoolName = student.SchoolName;
            SchoolYear = student.SchoolYear.Description();
            SchoolShift = student.SchoolShift.Description();
            Student = student;
        }
    }
}
