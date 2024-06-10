using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class Monedum
{
    public int IdMoneda { get; set; }

    public string? Nombre { get; set; }

    public string? Simbolo { get; set; }

    public int? Estado { get; set; }

    public virtual ICollection<TipoBobinaFo> TipoBobinaFos { get; set; } = new List<TipoBobinaFo>();

    public virtual ICollection<TipoHerraje> TipoHerrajes { get; set; } = new List<TipoHerraje>();
}
