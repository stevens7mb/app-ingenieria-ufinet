using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class ServiceDeskTicketStatusHistory
{
    public int IdTicketStatusHistory { get; set; }

    public int? IdPrefix { get; set; }

    public int? IdTicket { get; set; }

    public int IdTicketState { get; set; }

    public DateTime ChangeDateTime { get; set; }

    public int ChangedByEngineerId { get; set; }

    public string? ChangedByUser { get; set; }

    public virtual Engineer ChangedByEngineer { get; set; } = null!;

    public virtual Usuario? ChangedByUserNavigation { get; set; }

    public virtual TicketState IdTicketStateNavigation { get; set; } = null!;

    public virtual ServiceDeskTicket? ServiceDeskTicket { get; set; }
}
