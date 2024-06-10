using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class ReferenciaGeografica
{
    public int IdReferenciaGeografica { get; set; }

    public string? NombreDepartamento { get; set; }

    public string? NombreMunicipio { get; set; }

    public int? Estado { get; set; }
}
