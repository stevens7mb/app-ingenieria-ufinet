using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class InspeccionTrabajo
{
    public int IdInspeccionTrabajo { get; set; }

    public string IdServicioNuevo { get; set; } = null!;

    public int IdContrata { get; set; }

    public int IdTecnico { get; set; }

    public int IdIngeniero { get; set; }

    public DateTime FechaInspeccion { get; set; }

    public string? Observaciones { get; set; }

    public DateTime? FechaRevision { get; set; }

    public int? SncumpleNormas { get; set; }

    public int? SnaceptacionPreliminar { get; set; }

    public string UsuarioInspeccion { get; set; } = null!;

    public string? UsuarioSupervision { get; set; }

    public int IdInspeccionTrabajoEstado { get; set; }

    public virtual Contratum IdContrataNavigation { get; set; } = null!;

    public virtual Provisioning IdIngenieroNavigation { get; set; } = null!;

    public virtual InspeccionTrabajoEstado IdInspeccionTrabajoEstadoNavigation { get; set; } = null!;

    public virtual Tecnico IdTecnicoNavigation { get; set; } = null!;

    public virtual ICollection<InspeccionTrabajoTarea> InspeccionTrabajoTareas { get; set; } = new List<InspeccionTrabajoTarea>();

    public virtual ICollection<SupervisionTrabajo> SupervisionTrabajos { get; set; } = new List<SupervisionTrabajo>();

    public virtual Usuario UsuarioInspeccionNavigation { get; set; } = null!;

    public virtual Usuario? UsuarioSupervisionNavigation { get; set; }
}
