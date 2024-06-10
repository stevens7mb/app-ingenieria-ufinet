using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class ServiceDeskTicketLog
{
    public int IdTicketLog { get; set; }

    public int? IdPrefix { get; set; }

    public int? IdTicket { get; set; }

    public string Comment { get; set; } = null!;

    public int EngineerId { get; set; }

    public DateTime RegistryDate { get; set; }

    public virtual Engineer Engineer { get; set; } = null!;

    public virtual ServiceDeskTicket? ServiceDeskTicket { get; set; }
}
