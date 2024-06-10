using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class State
{
    public int IdState { get; set; }

    public string Name { get; set; } = null!;

    public int IdRegion { get; set; }

    public virtual Region IdRegionNavigation { get; set; } = null!;

    public virtual ICollection<Municipality> Municipalities { get; set; } = new List<Municipality>();

    public virtual ICollection<ServiceDeskTicket> ServiceDeskTickets { get; set; } = new List<ServiceDeskTicket>();
}
