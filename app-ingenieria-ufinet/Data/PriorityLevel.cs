using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class PriorityLevel
{
    public int IdPriorityLevel { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<ServiceDeskTicket> ServiceDeskTickets { get; set; } = new List<ServiceDeskTicket>();
}
