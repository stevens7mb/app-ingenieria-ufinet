using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class IncidentType
{
    public int IdIncidentType { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<ServiceDeskTicket> ServiceDeskTickets { get; set; } = new List<ServiceDeskTicket>();
}
