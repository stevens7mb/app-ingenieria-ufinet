using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class PidetalleHerraje
{
    public int IdPidetalleHerraje { get; set; }

    public int IdTipoHerraje { get; set; }

    public int IdPidetalleFo { get; set; }

    public int NumPi { get; set; }

    public decimal? CantidadHerrajes { get; set; }

    public decimal? PrecioUnitarioHerraje { get; set; }

    public decimal? TotalDetalleCompraHerraje { get; set; }

    public string? Usuario { get; set; }

    public virtual PidetalleFo IdPidetalleFoNavigation { get; set; } = null!;

    public virtual TipoHerraje IdTipoHerrajeNavigation { get; set; } = null!;

    public virtual Usuario? UsuarioNavigation { get; set; }
}
