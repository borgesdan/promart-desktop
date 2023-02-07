using System;
using System.Windows;

namespace Promart.Core
{
    public static class Error
    {
        public static void ShowDatabaseError(string message, Exception ex)
        {
            MessageBox.Show(
                $"{message}\n" +
                "O banco de dados pode estar desconectado ou não foi possível acessar o banco de dados.\n\n\n" +
                $"Erro completo: {ex.Message}",
                "Ação não executada",
                MessageBoxButton.OK,
                MessageBoxImage.Error
                );
        }
    }
}
