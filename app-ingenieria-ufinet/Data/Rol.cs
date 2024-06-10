using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class Rol
{
    public int IdRol { get; set; }

    public string Descripcion { get; set; } = null!;

    public int SnappContrata { get; set; }

    public int SnappInspeccion { get; set; }

    public int SnappSupervision { get; set; }

    public int SnmaintanceEngineer { get; set; }
}
