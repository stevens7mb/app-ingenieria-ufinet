using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class Kam
{
    public int IdKam { get; set; }

    public string? Nombre { get; set; }

    public string? Usuario { get; set; }

    public int? Estado { get; set; }

    public int? IdSucursal { get; set; }

    public virtual ICollection<Factibilidad> Factibilidads { get; set; } = new List<Factibilidad>();

    public virtual Sucursal? IdSucursalNavigation { get; set; }
}
