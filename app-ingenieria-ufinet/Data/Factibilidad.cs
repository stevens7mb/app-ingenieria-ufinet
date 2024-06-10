using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class Factibilidad
{
    public int IdFactibilidad { get; set; }

    public int Ticket { get; set; }

    public string? Estudio { get; set; }

    public int IdCliente { get; set; }

    public int IdKam { get; set; }

    public int? Bw { get; set; }

    public DateTime? FechaSolicitud { get; set; }

    public DateTime? FechaRespuesta { get; set; }

    public int? Estado { get; set; }

    public int? SitioConCobertura { get; set; }

    public int? SitioConCoberturaParcial { get; set; }

    public int? SitioSinCobertura { get; set; }

    public int IdIngeniero { get; set; }

    public int? IdTipoServicio { get; set; }

    public int? IdSucursal { get; set; }

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Provisioning IdIngenieroNavigation { get; set; } = null!;

    public virtual Kam IdKamNavigation { get; set; } = null!;

    public virtual Sucursal? IdSucursalNavigation { get; set; }

    public virtual TipoServicio? IdTipoServicioNavigation { get; set; }
}
