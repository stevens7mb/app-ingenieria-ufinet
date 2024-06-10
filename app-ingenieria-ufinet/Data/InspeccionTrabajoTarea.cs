using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class InspeccionTrabajoTarea
{
    public int IdInspeccionTrabajoTarea { get; set; }

    public int IdInspeccionTrabajo { get; set; }

    public string IdServicioNuevo { get; set; } = null!;

    public int IdTarea { get; set; }

    public int IdRespuesta { get; set; }

    public string? ObservacionSupervision { get; set; }

    public virtual RespuestaTarea IdRespuestaNavigation { get; set; } = null!;

    public virtual TrabajoTarea IdTareaNavigation { get; set; } = null!;

    public virtual InspeccionTrabajo InspeccionTrabajo { get; set; } = null!;

    public virtual ICollection<SupervisionTrabajoTarea> SupervisionTrabajoTareas { get; set; } = new List<SupervisionTrabajoTarea>();
}
