using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class Piestado
{
    public int IdEstadoPi { get; set; }

    public string? DescripcionEstadoPi { get; set; }

    public virtual ICollection<PiresumenCompra> PiresumenCompras { get; set; } = new List<PiresumenCompra>();
}
