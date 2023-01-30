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

                string sql = @$"BACKUP DATABASE [{databaseName}] TO DISK = N'{destinationFilePath}' WITH NOFORMAT, NOINIT, NAME = N'{databaseName}', SKIP, NOREWIND, NOUNLOAD, STATS = 10;";

                var command = new SqlCommand(sql, conn);
                await command.ExecuteNonQueryAsync();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro ao tentar realizar o backup.\n\n{ex.Message}");
                return false;
            }
        }

        public static async Task<bool> RestoreDatabase(string databaseName, string connectionString, string filePath)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();

                string sql = @$"RESTORE DATABASE [{databaseName}] FROM DISK = N'{filePath}' WITH  FILE = 1, NOUNLOAD, STATS = 5;";
                
                var command = new SqlCommand(sql, conn);                
                await command.ExecuteNonQueryAsync();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro ao tentar realizar a restauração.\n\n{ex.Message}");
                return false;
            }
        }
    }
}
