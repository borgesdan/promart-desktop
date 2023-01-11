using Promart.Database.Entities;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Documents;

namespace Promart.Core.Html
{
    public static class HtmlBuilder
    {
        public static string Create(Student student)
        {
            var file = File.ReadAllText(@"./templates/registro_aluno.xhtml");

            StringBuilder builder = new StringBuilder(file);

            builder.Replace("$$nome", student.FullName);
            builder.Replace("$$nascimento", student.BirthDate?.ToShortDateString());
            builder.Replace("$$sexo", student.Gender.Description());
            builder.Replace("$$rg", student.RG);
            builder.Replace("$$cpf", student.CPF);
            builder.Replace("$$certidao", student.Certidao);
            builder.Replace("$$responsavel", student.ResponsibleName);
            builder.Replace("$$vinculo", student.Relationship.Description());
            builder.Replace("$$moradia", student.Dwelling.Description());
            builder.Replace("$$renda", student.MonthlyIncome.Description());
            builder.Replace("$$bolsa", student.IsGovernmentBeneficiary == true ? "Sim" : "Não");
            builder.Replace("$$telefone", student.ResponsiblePhone);
            builder.Replace("$$rua", student.AddressStreet);
            builder.Replace("$$bairro", student.AddressDistrict);
            builder.Replace("$$num", student.AddressNumber);
            builder.Replace("$$complemento", student.AddressComplement);
            builder.Replace("$$referencia", student.AddressReferencePoint);
            builder.Replace("$$escola", student.SchoolName);
            builder.Replace("$$turno", student.SchoolShift.Description());
            builder.Replace("$$ano", student.SchoolYear.Description());
            builder.Replace("$$situacao", student.ProjectStatus.Description());
            builder.Replace("$$projeto_turno", student.ProjectShift.Description());
            builder.Replace("$$obs", student.Observations);

            var workshops = student.Workshops?.Select(s => s.Name).ToArray();

            if(workshops != null)
                builder.Replace("$$oficinas", string.Join("; ", workshops));            

            return builder.ToString();
        }
    }
}