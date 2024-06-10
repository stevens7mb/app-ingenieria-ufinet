using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class SupervisionTrabajo
{
    public int IdSupervision { get; set; }

    public int IdInspeccionTrabajo { get; set; }

    public string IdServicioNuevo { get; set; } = null!;

    public DateTime FechaSupervision { get; set; }

    public string? Observaciones { get; set; }

    public string? UsuarioSupervision { get; set; }

    public int? Snaceptada { get; set; }

    public virtual InspeccionTrabajo InspeccionTrabajo { get; set; } = null!;

    public virtual ICollection<SupervisionTrabajoTarea> SupervisionTrabajoTareas { get; set; } = new List<SupervisionTrabajoTarea>();

    public virtual Usuario? UsuarioSupervisionNavigation { get; set; }
}
