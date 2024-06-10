using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class PidetalleFo
{
    public int IdPidetalleFo { get; set; }

    public int IdPiresumenCompra { get; set; }

    public int NumPi { get; set; }

    public int? IdTipoBobinaFo { get; set; }

    public decimal? DistanciaPidetalle { get; set; }

    public decimal? CantidadBobinas { get; set; }

    public decimal? PrecioUnitario { get; set; }

    public decimal? TotalDetalleFopi { get; set; }

    public string? Usuario { get; set; }

    public virtual TipoBobinaFo? IdTipoBobinaFoNavigation { get; set; }

    public virtual ICollection<PidetalleHerraje> PidetalleHerrajes { get; set; } = new List<PidetalleHerraje>();

    public virtual PiresumenCompra PiresumenCompra { get; set; } = null!;

    public virtual Usuario? UsuarioNavigation { get; set; }
}
