using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class RespuestaTarea
{
    public int IdRespuesta { get; set; }

    public string Respuesta { get; set; } = null!;

    public int Estado { get; set; }

    public virtual ICollection<InspeccionTrabajoTarea> InspeccionTrabajoTareas { get; set; } = new List<InspeccionTrabajoTarea>();
}
