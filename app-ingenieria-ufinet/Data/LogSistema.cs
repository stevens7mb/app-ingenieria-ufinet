using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class LogSistema
{
    public int IdLog { get; set; }

    public string? Usuario { get; set; }

    public string NombreTabla { get; set; } = null!;

    public string NombreSp { get; set; } = null!;

    public DateTime FechaTransaccion { get; set; }

    public string TipoTransaccion { get; set; } = null!;

    public int? RegistrosAfectados { get; set; }

    public string? DescripcionLog { get; set; }

    public int? IdPiresumenCompra { get; set; }

    public int? NumPi { get; set; }

    public virtual Usuario? UsuarioNavigation { get; set; }
}
