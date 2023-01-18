using System.ComponentModel;

namespace Promart.Database
{
    public enum RegistryStatus
    {
        [Description("Ativo")]
        Active,
        [Description("Inativo")]
        Inactive,
        [Description("Deletado")]
        Deleted,
    }
}
