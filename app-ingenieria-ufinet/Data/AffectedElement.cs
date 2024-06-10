using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class AffectedElement
{
    public int IdAffectedElement { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<ServiceDeskTicket> ServiceDeskTickets { get; set; } = new List<ServiceDeskTicket>();
}
