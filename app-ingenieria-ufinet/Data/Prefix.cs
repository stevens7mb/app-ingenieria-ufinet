using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class Prefix
{
    public int IdPrefix { get; set; }

    public string? PrefixDesc { get; set; }

    public virtual ICollection<ServiceDeskTicket> ServiceDeskTickets { get; set; } = new List<ServiceDeskTicket>();
}
