using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class MaintanceEngineer
{
    public int IdMaintanceEngineer { get; set; }

    public string? Name { get; set; }

    public string UserName { get; set; } = null!;

    public int? ActiveStatus { get; set; }

    public virtual ICollection<ServiceDeskTicket> ServiceDeskTickets { get; set; } = new List<ServiceDeskTicket>();

    public virtual Usuario UserNameNavigation { get; set; } = null!;
}
