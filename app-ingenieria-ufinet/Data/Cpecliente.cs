using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class Cpecliente
{
    public int IdCpecliente { get; set; }

    public string? TipoCpecliente { get; set; }

    public string? NombreCpecliente { get; set; }

    public int? Estado { get; set; }
}
