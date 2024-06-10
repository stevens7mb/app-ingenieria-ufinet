using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class Usuario
{
    public int Correlativo { get; set; }

    public string Usuario1 { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string? Email { get; set; }

    public int? IdSucursal { get; set; }

    public int? Activo { get; set; }

    public virtual ICollection<Engineer> Engineers { get; set; } = new List<Engineer>();

    public virtual Sucursal? IdSucursalNavigation { get; set; }

    public virtual ICollection<InspeccionTrabajo> InspeccionTrabajoUsuarioInspeccionNavigations { get; set; } = new List<InspeccionTrabajo>();

    public virtual ICollection<InspeccionTrabajo> InspeccionTrabajoUsuarioSupervisionNavigations { get; set; } = new List<InspeccionTrabajo>();

    public virtual ICollection<LogSistema> LogSistemas { get; set; } = new List<LogSistema>();

    public virtual ICollection<PidetalleFo> PidetalleFos { get; set; } = new List<PidetalleFo>();

    public virtual ICollection<PidetalleHerraje> PidetalleHerrajes { get; set; } = new List<PidetalleHerraje>();

    public virtual ICollection<PiresumenCompra> PiresumenCompras { get; set; } = new List<PiresumenCompra>();

    public virtual ICollection<Provisioning> Provisionings { get; set; } = new List<Provisioning>();

    public virtual ICollection<ServiceDeskTicketStatusHistory> ServiceDeskTicketStatusHistories { get; set; } = new List<ServiceDeskTicketStatusHistory>();

    public virtual ICollection<SesionSistema> SesionSistemas { get; set; } = new List<SesionSistema>();

    public virtual ICollection<SupervisionTrabajo> SupervisionTrabajos { get; set; } = new List<SupervisionTrabajo>();

    public virtual ICollection<Tecnico> Tecnicos { get; set; } = new List<Tecnico>();
}
