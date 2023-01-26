using Microsoft.Data.SqlClient;
using Promart.Database.Context;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace Promart.Core
{
    public class Backup
    {
        public async Task<bool> FromDatabase(string databaseName, string connectionString, string destinationFilePath)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();

                string sql = @$"                    
                    BACKUP DATABASE [{databaseName}]
                    TO DISK = N'{destinationFilePath}' 
                    WITH NOFORMAT, NOINIT,
                    NAME = N'PromarDB-Full Database Backup', SKIP, NOREWIND, NOUNLOAD, STATS = 10;
                ";

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
    }
}
