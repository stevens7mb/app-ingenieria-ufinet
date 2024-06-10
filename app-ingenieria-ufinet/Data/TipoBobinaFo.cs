using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class TipoBobinaFo
{
    public int IdTipoBobinaFo { get; set; }

    public string TipoBobinaFo1 { get; set; } = null!;

    public decimal? Precio { get; set; }

    public int IdUnidadMedidaPrecio { get; set; }

    public int IdMoneda { get; set; }

    public decimal DistanciaBobina { get; set; }

    public int IdUnidadMedidaBobina { get; set; }

    public int? Estado { get; set; }

    public virtual Monedum IdMonedaNavigation { get; set; } = null!;

    public virtual UnidadesMedidaBobina IdUnidadMedidaBobinaNavigation { get; set; } = null!;

    public virtual UnidadesMedidaPrecio IdUnidadMedidaPrecioNavigation { get; set; } = null!;

    public virtual ICollection<PidetalleFo> PidetalleFos { get; set; } = new List<PidetalleFo>();

    public virtual ICollection<TipoHerraje> TipoHerrajes { get; set; } = new List<TipoHerraje>();
}
