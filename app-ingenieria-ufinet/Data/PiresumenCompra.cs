using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class PiresumenCompra
{
    public int IdPiresumenCompra { get; set; }

    public int NumPi { get; set; }

    public string? NombrePi { get; set; }

    public string? ComentarioPi { get; set; }

    public DateTime? FechaSolicitud { get; set; }

    public DateTime? FechaOc { get; set; }

    public DateTime? FechaBodegaSucursal { get; set; }

    public int? IdSucursal { get; set; }

    public decimal? TotalPiresumenCompra { get; set; }

    public string? Usuario { get; set; }

    public int IdEstadoPi { get; set; }

    public int Estado { get; set; }

    public decimal TotalPifo { get; set; }

    public decimal TotalPiherrajes { get; set; }

    public decimal TotalPi { get; set; }

    public DateTime? FechaGeneracion { get; set; }

    public decimal? Incurrido { get; set; }

    public decimal? PendienteIncurrir { get; set; }

    public virtual Piestado IdEstadoPiNavigation { get; set; } = null!;

    public virtual Sucursal? IdSucursalNavigation { get; set; }

    public virtual ICollection<PidetalleFo> PidetalleFos { get; set; } = new List<PidetalleFo>();

    public virtual Usuario? UsuarioNavigation { get; set; }
}
