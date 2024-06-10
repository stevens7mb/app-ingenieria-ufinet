using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class SupervisionTrabajoTarea
{
    public int IdSupervisionTarea { get; set; }

    public int IdSupervision { get; set; }

    public int IdInspeccionTrabajoTarea { get; set; }

    public string Observaciones { get; set; } = null!;

    public virtual InspeccionTrabajoTarea IdInspeccionTrabajoTareaNavigation { get; set; } = null!;

    public virtual SupervisionTrabajo IdSupervisionNavigation { get; set; } = null!;
}
