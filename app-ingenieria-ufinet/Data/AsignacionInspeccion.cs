using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class AsignacionInspeccion
{
    public string IdServicioNuevo { get; set; } = null!;

    public int? IdCliente { get; set; }

    public int? IdTecnico { get; set; }

    public int? IdContrata { get; set; }

    public DateTime? Fecha { get; set; }

    public int? IdIngeniero { get; set; }

    public virtual Contratum? IdContrataNavigation { get; set; }

    public virtual Provisioning? IdIngenieroNavigation { get; set; }

    public virtual Tecnico? IdTecnicoNavigation { get; set; }
}
