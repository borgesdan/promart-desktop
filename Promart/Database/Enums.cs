using System.ComponentModel;

namespace Promart.Database
{
    /// <summary>
    /// </summary>
    public enum GenderType : byte
    {
        [Description("Masculino")]
        Masculino,
        [Description("Feminino")]
        Feminino,
        [Description("Não informado")]
        Indefinido
    }

    /// <summary>
    /// Define com quem o aluno reside junto, a relação mais superior.
    /// </summary>
    public enum StudentRelationshipType : byte
    {
        [Description("Pais")]
        Pais,
        [Description("Avós")]
        Avos,
        [Description("Somente a mãe")]
        Somente_Mae,
        [Description("Somente o pai")]
        Somente_Pai,
        [Description("Mãe e padrasto")]
        Mae_Padrasto,
        [Description("Pai e madrasta")]
        Pai_Madrasta,
        [Description("Tios")]
        Tios,
        [Description("Outro")]
        Outro,
        [Description("Não informado")]
        Indefinido,
    }

    public enum FamilyRelationshipType : byte
    {
        [Description("Pai")]
        Pai,
        [Description("Mãe")]
        Mae,
        [Description("Filho")]
        Filho,
        [Description("Irmão")]
        Irmao,
        [Description("Avô")]
        Avo,
        [Description("Bisavô")]
        Bisavo,
        [Description("Tio")]
        Tio,
        [Description("Neto")]
        Neto,
        [Description("Primo")]
        Primo,
        [Description("Outro")]
        Outro,
        [Description("Não informado")]
        Indefinido
    }

    /// <summary>
    /// Obtém ou define o turno escolar.
    /// </summary>
    public enum SchoolShiftType : byte
    {
        [Description("Matutino")]
        Matutino,
        [Description("Vespertino")]
        Vespertino,
        [Description("Não informado")]
        Indefinido
    }

    /// <summary>
    /// Obtém ou define o tipo de moradia.
    /// </summary>
    public enum DwellingType : byte
    {
        [Description("Própria")]
        Propria,
        [Description("Alugada")]
        Alugada,
        [Description("Cedida")]
        Cedida,
        [Description("Outro")]
        Outro,
        [Description("Não informado")]
        Indefinido,
    }

    /// <summary>
    /// Obtém ou define a faixa salárial da renda mensal.
    /// </summary>      
    public enum MonthlyIncomeType : byte
    {
        [Description("Menor que 1/2 salário")]
        Menor_Meio_Salario_Minimo,
        [Description("1/2 salário")]
        Meio_Salario_Minimo,
        [Description("1 salário")]
        Salario_Minimo,
        [Description("2 ou mais salários")]
        Dois_Salarios_Ou_Mais,
        [Description("Não informado")]
        Indefinido
    }

    /// <summary>
    /// Obtém ou define a situação do aluno no projeto.s
    /// </summary>
    public enum ProjectStatusType : byte
    {
        [Description("Inscrito")]
        Inscrito,
        [Description("Aprovado")]
        Aprovado,
        [Description("Em espera")]
        Espera,
        [Description("Matriculado")]
        Matriculado,
        [Description("Não aprovado")]
        Desaprovado,
        [Description("Desistente")]
        Desistente,
        [Description("Ex aluno")]
        ExAluno,
        [Description("Não informado")]
        Indefinido
    }

    public enum SchoolYearType : byte
    {
        [Description("1º ano fundamental")]
        Fundamental1,
        [Description("2º ano fundamental")]
        Fundamental2,
        [Description("3º ano fundamental")]
        Fundamental3,
        [Description("4º ano fundamental")]
        Fundamental4,
        [Description("5º ano fundamental")]
        Fundamental5,
        [Description("6º ano fundamental")]
        Fundamental6,
        [Description("7º ano fundamental")]
        Fundamental7,
        [Description("8º ano fundamental")]
        Fundamental8,
        [Description("9º ano fundamental")]
        Fundamental9,
        [Description("1º ano médio")]
        Medio1,
        [Description("2º ano médio")]
        Medio2,
        [Description("3º ano médio")]
        Medio3,
        [Description("Outro ano ou ciclo")]
        Outro,
        [Description("Não informado")]
        Indefinido
    }
}
