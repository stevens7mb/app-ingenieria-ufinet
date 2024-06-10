using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class ServiceDeskTicketFile
{
    public int TicketFileId { get; set; }

    public string NameFile { get; set; } = null!;

    public string PathFile { get; set; } = null!;

    public long FileSize { get; set; }

    public int? IdPrefix { get; set; }

    public int IdTicket { get; set; }

    public virtual ServiceDeskTicket? ServiceDeskTicket { get; set; }
}
