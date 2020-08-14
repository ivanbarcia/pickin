using System.ComponentModel;


namespace Pickin.Enums
{
    public enum States
    {
        [Description("Alta")]
        Created = 1,
        [Description("Modificado")]
        Modified = 2,
        [Description("Baja")]
        Deleted = 3,
        [Description("Cancelado")]
        Cancellation = 4,
        [Description("Inactivo")]
        Inactive = 5,
        [Description("Pendiente de Autorizar")]
        PendingChanges = 9
    };


}
