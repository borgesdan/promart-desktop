using Microsoft.Data.SqlClient;
using System;
using System.Threading.Tasks;

namespace Promart.Core
{
    public class Backup
    {
        public static async Task<bool> FromDatabase(string databaseName, string connectionString, string destinationFilePath)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();

                string sql = @$"BACKUP DATABASE [{databaseName}] TO DISK = N'{destinationFilePath}'";

                var command = new SqlCommand(sql, conn);
                await command.ExecuteNonQueryAsync();

                return true;
            }
            catch (Exception ex)
            {
                Error.ShowDatabaseError($"Ocorreu um erro ao tentar realizar o backup do banco de dados.", ex);
                return false;
            }
        }

        public static async Task<bool> RestoreDatabase(string databaseName, string connectionString, string filePath)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();
                
                string sql = @$"DROP DATABASE IF EXISTS [{databaseName}]; RESTORE DATABASE [{databaseName}] FROM DISK = N'{filePath}'";
                
                var command = new SqlCommand(sql, conn);                
                await command.ExecuteNonQueryAsync();

                return true;
            }
            catch (Exception ex)
            {
                Error.ShowDatabaseError($"Ocorreu um erro ao tentar restaurar o backup do banco de dados.", ex);
                return false;
            }
        }

        public static async Task<bool> MigrateOldDatabase(string connectionString)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();

                string sql = @"
                    INSERT into [PromartDesktop].dbo.Workshops(Name, Description, RegistryStatus, OldId)
					SELECT Nome, Descricao, 0, Id
					from [PromartBD].dbo.Oficinas;

					INSERT into [PromartDesktop].dbo.Students	
						(
							FullName, BirthDate, Gender, RG, CPF, Certidao,
							ResponsibleName, Relationship, 
							IsGovernmentBeneficiary, Dwelling, MonthlyIncome,
							SchoolName, SchoolYear, SchoolShift,
							AddressStreet, AddressDistrict, AddressNumber, AddressCity, AddressState,
							AddressCEP, AddressReferencePoint,
							ProjectStatus, ProjectShift, Observations,
							ProjectRegistry, ProjectRegistryDate, RegistryStatus, OldId
						)
						SELECT 
							NomeCompleto,
							DataNascimento,
							Sexo,
							RG,
							CPF,
							Certidao,
							NomeResponsavel,
							VinculoFamiliar,
							IsBeneficiario,
							TipoMoradia,
							Renda,
							EscolaNome,
							AnoEscolar,
							TurnoEscolar,
							EnderecoRua,
							EnderecoBairro,
							EnderecoNumero,
							EnderecoCidade,
							EnderecoEstado,
							EnderecoCEP,
							EnderecoComplemento,
							SituacaoProjeto,
							TurnoProjeto,
							Observacoes,
							Matricula, 
							DataMatricula,
							0,
							Id
						from [PromartBD].dbo.Alunos;

					WITH Student_Ids (StudentId, StudentOldId) AS (SELECT Id, OldId FROM [PromartDesktop].dbo.Students)

					INSERT INTO [PromartDesktop].dbo.FamilyRelationships(
							FullName, Age, Income, Occupation, Schooling, Relationship, RegistryStatus, StudentId
						)
						SELECT
							NomeFamiliar, Idade, Renda, Ocupacao, Escolaridade, 9, 0, StudentId
						FROM [PromartBD].dbo.AlunoVinculos as vinculoFamiliar
						LEFT JOIN Student_Ids
							ON vinculoFamiliar.IdAluno = Student_Ids.StudentOldId;

					WITH Workshop_Relationships (WorkshopId, WorkshopOldId) 
						AS (		
						SELECT Id, OldId FROM [PromartDesktop].dbo.Workshops 
						),
						Student_Ids (StudentId, StudentOldId) AS (SELECT Id, OldId FROM [PromartDesktop].dbo.Students)

					INSERT INTO [PromartDesktop].dbo.StudentWorkshop(StudentsId, WorkshopsId)
						SELECT
							StudentId, WorkshopId
						FROM [PromartBD].dbo.AlunoOficinas as vinculoOficina
						LEFT JOIN Workshop_Relationships
							ON vinculoOficina.IdOficina = Workshop_Relationships.WorkshopOldId
						LEFT JOIN Student_Ids
							ON vinculoOficina.IdAluno = Student_Ids.StudentOldId;
                ";

                var command = new SqlCommand(sql, conn);
                await command.ExecuteNonQueryAsync();

                return true;
            }
            catch(Exception ex)
            {
                Error.ShowDatabaseError($"Ocorreu um erro ao tentar migrar os dados.", ex);
                return false;
            }
        }
    }
}
