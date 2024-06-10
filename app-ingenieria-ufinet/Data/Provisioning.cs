﻿using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class Provisioning
{
    public int IdIngeniero { get; set; }

    public string? Nombre { get; set; }

    public string? Usuario { get; set; }

    public int? Estado { get; set; }

    public virtual ICollection<AsignacionInspeccion> AsignacionInspeccions { get; set; } = new List<AsignacionInspeccion>();

    public virtual ICollection<Factibilidad> Factibilidads { get; set; } = new List<Factibilidad>();

    public virtual ICollection<InspeccionTrabajo> InspeccionTrabajos { get; set; } = new List<InspeccionTrabajo>();

    public virtual Usuario? UsuarioNavigation { get; set; }
}
