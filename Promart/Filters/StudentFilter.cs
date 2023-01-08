using System.ComponentModel;

namespace Promart.Filters
{
    public class StudentFilter
    {
        [DisplayName("Nome")]
        public string? FullName { get; set; }
        [DisplayName("Sexo")]
        public string? Gender { get; set; }
        [DisplayName("Responsável")]
        public string? ResponsibleName { get; set; }
        [DisplayName("Telefone")]
        public string? ResponsiblePhone { get; set; }
        [DisplayName("Matrícula")]
        public string? Registry { get; set; }
        [DisplayName("Data Matrícula")]
        public string? RegistryDate { get; set; }
    }
}
