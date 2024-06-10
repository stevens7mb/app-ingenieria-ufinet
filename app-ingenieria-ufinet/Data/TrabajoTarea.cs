using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class TrabajoTarea
{
    public int IdTarea { get; set; }

    public string Tarea { get; set; } = null!;

    public int IdCategoriaTarea { get; set; }

    public int Estado { get; set; }

    public virtual CategoriaTarea IdCategoriaTareaNavigation { get; set; } = null!;

    public virtual ICollection<InspeccionTrabajoTarea> InspeccionTrabajoTareas { get; set; } = new List<InspeccionTrabajoTarea>();
}
