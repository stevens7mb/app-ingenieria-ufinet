using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class Region
{
    public int IdRegion { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Engineer> Engineers { get; set; } = new List<Engineer>();

    public virtual ICollection<ServiceDeskTicket> ServiceDeskTickets { get; set; } = new List<ServiceDeskTicket>();

    public virtual ICollection<State> States { get; set; } = new List<State>();
}
