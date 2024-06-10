using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class SesionSistema
{
    public int IdSesionSistema { get; set; }

    public string? Usuario { get; set; }

    public DateTime Fecha { get; set; }

    public int? IdTipoSesion { get; set; }

    public string? Descripcion { get; set; }

    public virtual TipoSesion? IdTipoSesionNavigation { get; set; }

    public virtual Usuario? UsuarioNavigation { get; set; }
}
