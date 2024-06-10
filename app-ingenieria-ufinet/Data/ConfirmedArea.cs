using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class ConfirmedArea
{
    public int IdConfirmedArea { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<ServiceDeskTicket> ServiceDeskTickets { get; set; } = new List<ServiceDeskTicket>();
}
