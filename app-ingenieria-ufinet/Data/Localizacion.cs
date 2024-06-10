using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class Localizacion
{
    public int IdLocalizacion { get; set; }

    public string? TipoLocalizacion { get; set; }

    public int? Estado { get; set; }
}
