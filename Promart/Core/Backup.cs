using Microsoft.Data.SqlClient;
using System;
using System.Threading.Tasks;
using System.Windows;

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
    }
}
