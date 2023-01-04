using System.ComponentModel;

namespace Promart.Database
{
    /// <summary>
    /// </summary>
    public enum GenderType : byte
    {
        [Description("Masculino")]
        Masculino = 0,
        [Description("Feminino")]
        Feminino = 1,
        [Description("Não informado")]
        Indefinido = 99
    }

    /// <summary>
    /// Define com quem o aluno reside junto, a relação mais superior.
    /// </summary>
    public enum StudentRelationshipType : byte
    {
        [Description("Pais")]
        Pais = 0,
        [Description("Avós")]
        Avos = 1,
        [Description("Somente a mãe")]
        Somente_Mae = 2,
        [Description("Somente o pai")]
        Somente_Pai = 3,
        [Description("Mãe e padrasto")]
        Mae_Padrasto = 4,
        [Description("Pai e madrasta")]
        Pai_Madrasta = 5,
        [Description("Tios")]
        Tios = 6,
        [Description("Outro")]
        Outro = 7,
        [Description("Não informado")]
        Indefinido = 99,
    }

    public enum FamilyRelationshipType : byte
    {
        [Description("Pai")]
        Pai = 0,
        [Description("Mãe")]
        Mae = 1,
        [Description("Filho")]
        Filho = 2,
        [Description("Irmão")]
        Irmao = 3,
        [Description("Avô")]
        Avo = 4,
        [Description("Bisavô")]
        Bisavo = 5,
        [Description("Tio")]
        Tio = 6,
        [Description("Neto")]
        Neto = 7,
        [Description("Primo")]
        Primo = 8,
        [Description("Outro")]
        Outro = 9,
        [Description("Não informado")]
        Indefinido = 99
    }

    /// <summary>
    /// Obtém ou define o turno escolar.
    /// </summary>
    public enum SchoolShiftType : byte
    {
        [Description("Matutino")]
        Matutino = 0,
        [Description("Vespertino")]
        Vespertino = 1,
        [Description("Não informado")]
        Indefinido = 99
    }

    /// <summary>
    /// Obtém ou define o tipo de moradia.
    /// </summary>
    public enum DwellingType : byte
    {
        [Description("Própria")]
        Propria = 0,
        [Description("Alugada")]
        Alugada = 1,
        [Description("Cedida")]
        Cedida = 2,
        [Description("Outro")]
        Outro = 3,
        [Description("Não informado")]
        Indefinido = 99,
    }

    /// <summary>
    /// Obtém ou define a faixa salárial da renda mensal.
    /// </summary>      
    public enum MonthlyIncomeType : byte
    {
        [Description("Menor que 1/2 salário")]
        Menor_Meio_Salario_Minimo = 0,
        [Description("1/2 salário")]
        Meio_Salario_Minimo = 1,
        [Description("1 salário")]
        Salario_Minimo = 2,
        [Description("2 ou mais salários")]        
        Dois_Salarios_Ou_Mais = 4,
        [Description("Não informado")]
        Indefinido = 99
    }

    /// <summary>
    /// Obtém ou define a situação do aluno no projeto.s
    /// </summary>
    public enum ProjectStatusType : byte
    {
        [Description("Inscrito")]
        Inscrito = 0,
        [Description("Aprovado")]
        Aprovado = 1,
        [Description("Em espera")]
        Espera = 2,
        [Description("Matriculado")]
        Matriculado = 3,
        [Description("Não aprovado")]
        Desaprovado = 4,
        [Description("Desistente")]
        Desistente = 5,
        [Description("Ex aluno")]
        ExAluno = 6,
        [Description("Não informado")]
        Indefinido = 99
    }

    public enum SchoolYearType : byte
    {        
        [Description("1º ano fundamental")]
        Fundamental1 = 0,
        [Description("2º ano fundamental")]
        Fundamental2 = 1,
        [Description("3º ano fundamental")]
        Fundamental3 = 2,
        [Description("4º ano fundamental")]
        Fundamental4 = 3,
        [Description("5º ano fundamental")]
        Fundamental5 = 4,
        [Description("6º ano fundamental")]
        Fundamental6 = 5,
        [Description("7º ano fundamental")]
        Fundamental7 = 6,
        [Description("8º ano fundamental")]
        Fundamental8 = 7,
        [Description("9º ano fundamental")]
        Fundamental9 = 8,
        [Description("1º ano médio")]
        Medio1 = 9,
        [Description("2º ano médio")]
        Medio2 = 10,
        [Description("3º ano médio")]
        Medio3 = 11,
        [Description("Outro ano ou ciclo")]
        Outro = 98,
        [Description("Não informado")]
        Indefinido = 99
    }
}
