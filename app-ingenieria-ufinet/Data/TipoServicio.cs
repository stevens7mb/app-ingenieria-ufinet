using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class TipoServicio
{
    public int IdTipoServicio { get; set; }

    public string TipoServicio1 { get; set; } = null!;

    public int? Estado { get; set; }

    public virtual ICollection<Factibilidad> Factibilidads { get; set; } = new List<Factibilidad>();
}
