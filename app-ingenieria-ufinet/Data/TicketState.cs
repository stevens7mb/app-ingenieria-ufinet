using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class TicketState
{
    public int IdTicketState { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<ServiceDeskTicketStatusHistory> ServiceDeskTicketStatusHistories { get; set; } = new List<ServiceDeskTicketStatusHistory>();

    public virtual ICollection<ServiceDeskTicket> ServiceDeskTickets { get; set; } = new List<ServiceDeskTicket>();
}
