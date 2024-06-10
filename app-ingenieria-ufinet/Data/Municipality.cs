using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class Municipality
{
    public int IdMunicipality { get; set; }

    public string Name { get; set; } = null!;

    public int IdState { get; set; }

    public virtual State IdStateNavigation { get; set; } = null!;

    public virtual ICollection<ServiceDeskTicket> ServiceDeskTickets { get; set; } = new List<ServiceDeskTicket>();
}
