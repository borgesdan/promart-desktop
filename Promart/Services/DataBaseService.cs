using Promart.Core;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace Promart.Services
{
    public class DataBaseService
    {
        public async Task CreateBackup()
        {
            var now = DateTime.Now;

            var year = now.Year.ToString();
            var month = now.Month < 10 ? $"0{now.Month}" : now.Month.ToString();
            var day = now.Day < 10 ? $"0{now.Day}" : now.Day.ToString();
            var time = DateTime.Now.ToLongTimeString().Replace(":", "-");
            
            var destionationFile = System.IO.Path.Combine(Environment.CurrentDirectory, "Backups", $"{year}-{month}-{day}-{time}-promartdesktop.bak");
            
            var configuration = ConfigurationManager.Open() ?? throw new NullReferenceException("Não foi possível carregar o arquivo de configuração");
            
            var result = await DataBaseManager.FromDatabaseAsync("PromartDesktop", configuration.ConnectionStrings.Default, destionationFile);

            if (result == null)
                MessageBox.Show("Backup realizado com sucesso!", "Backup realizado", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                Error.ShowDatabaseError($"Ocorreu um erro ao tentar realizar o backup do banco de dados.", result);
        }
    }
}