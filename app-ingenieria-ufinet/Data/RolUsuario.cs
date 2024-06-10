using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class RolUsuario
{
    public string Usuario { get; set; } = null!;

    public int IdRol { get; set; }

    public virtual Rol IdRolNavigation { get; set; } = null!;

    public virtual Usuario UsuarioNavigation { get; set; } = null!;
}
