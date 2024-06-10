using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class CategoriaTarea
{
    public int IdCategoriaTarea { get; set; }

    public string Categoria { get; set; } = null!;

    public virtual ICollection<TrabajoTarea> TrabajoTareas { get; set; } = new List<TrabajoTarea>();
}
