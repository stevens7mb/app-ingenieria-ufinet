using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class Sucursal
{
    public int IdSucursal { get; set; }

    public string? Descripcion { get; set; }

    public string? Direccion { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual ICollection<Factibilidad> Factibilidads { get; set; } = new List<Factibilidad>();

    public virtual ICollection<Kam> Kams { get; set; } = new List<Kam>();

    public virtual ICollection<PiresumenCompra> PiresumenCompras { get; set; } = new List<PiresumenCompra>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
