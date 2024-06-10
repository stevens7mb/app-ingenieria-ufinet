using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class TipoSesion
{
    public int IdTipoSesion { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<SesionSistema> SesionSistemas { get; set; } = new List<SesionSistema>();
}
