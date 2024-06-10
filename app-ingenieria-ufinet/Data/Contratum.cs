using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class Contratum
{
    public int IdContrata { get; set; }

    public string? NombreContrata { get; set; }

    public int? Estado { get; set; }

    public virtual ICollection<AsignacionInspeccion> AsignacionInspeccions { get; set; } = new List<AsignacionInspeccion>();

    public virtual ICollection<InspeccionTrabajo> InspeccionTrabajos { get; set; } = new List<InspeccionTrabajo>();
}
