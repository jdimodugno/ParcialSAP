using System.ComponentModel;

namespace Domain.Enums
{
    public enum AccountTypes
    {
        [Description("Caja de Ahorro - CA")]
        Saving,

        [Description("Cuenta Corriente - CC")]
        Checking,
    }
}
