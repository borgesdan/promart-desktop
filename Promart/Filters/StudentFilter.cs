using Promart.Core;
using Promart.Database.Entities;
using System;
using System.ComponentModel;

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
        [DisplayName("Situação")]
        public string? Status { get; set; }
        [DisplayName("Matrícula")]
        public string? Registry { get; set; }
        [DisplayName("Data Matrícula")]
        public string? RegistryDate { get; set; }
        [DisplayName("Responsável")]
        public string? ResponsibleName { get; set; }
        [DisplayName("Telefone")]
        public string? ResponsiblePhone { get; set; }        

        public Student? Student { get; set; }

        public StudentFilter() { }

        public StudentFilter(Student student)
        {
            FullName = student.FullName;
            Gender = student.Gender.Description();
            Age = student.BirthDate != null ? ((int)((DateTime.Now - student.BirthDate.Value).TotalDays / 365)).ToString() : null;
            Status = student.ProjectStatus.Description();
            ResponsibleName = student.ResponsibleName;
            ResponsiblePhone = student.ResponsiblePhone;
            Registry = student.ProjectRegistry;
            RegistryDate = student.ProjectRegistryDate?.ToShortDateString();
            Student = student;
        }
    }
}
