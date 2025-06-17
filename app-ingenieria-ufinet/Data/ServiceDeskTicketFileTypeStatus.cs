using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class ServiceDeskTicketFileTypeStatus
{
    public int TypeStatusId { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<ServiceDeskTicketFile> ServiceDeskTicketFiles { get; set; } = new List<ServiceDeskTicketFile>();
}
