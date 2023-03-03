using System;
using System.Windows;

namespace Promart.Core
{
    public static class Error
    {
        public static void ShowDatabaseError(string message, Exception ex)
        {
            MessageBox.Show(
                $"{message}" +
                Environment.NewLine +
                Environment.NewLine +
                "O banco de dados pode estar desconectado, houve erro na execução dos dados ou não foi possível acessar o banco de dados." +
                Environment.NewLine +
                Environment.NewLine +
                Environment.NewLine +
                $"Erro completo: {ex.Message}",
                "Ação não executada",
                MessageBoxButton.OK,
                MessageBoxImage.Error
                );
        }
    }
}
