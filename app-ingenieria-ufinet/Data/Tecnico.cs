using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class Tecnico
{
    public int IdTecnico { get; set; }

    public string Nombre { get; set; } = null!;

    public string Usuario { get; set; } = null!;

    public int? Estado { get; set; }

    public virtual ICollection<AsignacionInspeccion> AsignacionInspeccions { get; set; } = new List<AsignacionInspeccion>();

    public virtual ICollection<InspeccionTrabajo> InspeccionTrabajos { get; set; } = new List<InspeccionTrabajo>();

    public virtual Usuario UsuarioNavigation { get; set; } = null!;
}
