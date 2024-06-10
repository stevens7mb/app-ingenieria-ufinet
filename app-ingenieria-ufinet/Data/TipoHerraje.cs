using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class TipoHerraje
{
    public int IdTipoHerraje { get; set; }

    public string TipoHerraje1 { get; set; } = null!;

    public decimal Precio { get; set; }

    public int IdMoneda { get; set; }

    public int IdTipoBobinaFo { get; set; }

    public int? Estado { get; set; }

    public string? Formula { get; set; }

    public virtual Monedum IdMonedaNavigation { get; set; } = null!;

    public virtual TipoBobinaFo IdTipoBobinaFoNavigation { get; set; } = null!;

    public virtual ICollection<PidetalleHerraje> PidetalleHerrajes { get; set; } = new List<PidetalleHerraje>();
}
