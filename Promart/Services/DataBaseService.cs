using Promart.Core;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

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
            
            var destionationFile = Path.Combine(Environment.CurrentDirectory, "Backups", $"{year}-{month}-{day}-{time}-promartdesktop.bak");
            
            var configuration = ConfigurationManager.Open() ?? throw new NullReferenceException("Não foi possível carregar o arquivo de configuração");
            
            var result = await DataBaseManager.FromDatabaseAsync("PromartDesktop", configuration.ConnectionStrings.Default, destionationFile);

            if (result == null)
                MessageBox.Show("Backup realizado com sucesso!", "Backup realizado", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                Error.ShowDatabaseError($"Ocorreu um erro ao tentar realizar o backup do banco de dados.", result);
        }

        public void OpenBackupFolder()
        {
            var destination = Path.Combine(Environment.CurrentDirectory, "Backups");

            Process.Start("explorer.exe", destination);
        }

        public async Task MigrateFromOldDataBase()
        {
            var messageResult = MessageBox.Show("Deseja migrar os dados antigos? Se já executou essa ação antes podem aparecer dados repetidos no banco de dados.", "Alerta!", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (messageResult != MessageBoxResult.Yes)
                return;

            var config = ConfigurationManager.Open() ?? throw new NullReferenceException("Não foi possível carregar o arquivo de configuração");

            var result = await DataBaseManager.MigrateOldDatabaseAsync(config.ConnectionStrings.Default);

            if (result != null)
                Error.ShowDatabaseError($"Ocorreu um erro ao tentar migrar os dados.", result);
            else
                MessageBox.Show("Migração realizada com sucesso.", "Migração", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}