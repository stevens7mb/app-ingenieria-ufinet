using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class Nniacceso
{
    public int IdNniacceso { get; set; }

    public string? NniaccesoDestino { get; set; }

    public string? EquipoAccesoDestino { get; set; }

    public string? IpaccesoDestino { get; set; }

    public int PuertoAccesoDestino { get; set; }

    public int? Estado { get; set; }
}
