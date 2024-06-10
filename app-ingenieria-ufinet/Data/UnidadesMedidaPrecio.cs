using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class UnidadesMedidaPrecio
{
    public int IdUnidadMedidaPrecio { get; set; }

    public string? UnidadMedida { get; set; }

    public string? UnidadMedidaCorta { get; set; }

    public int? Estado { get; set; }

    public virtual ICollection<TipoBobinaFo> TipoBobinaFos { get; set; } = new List<TipoBobinaFo>();
}
