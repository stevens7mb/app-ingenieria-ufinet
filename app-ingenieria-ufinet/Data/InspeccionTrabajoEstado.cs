using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class InspeccionTrabajoEstado
{
    public int IdInspeccionTrabajoEstado { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<InspeccionTrabajo> InspeccionTrabajos { get; set; } = new List<InspeccionTrabajo>();
}
