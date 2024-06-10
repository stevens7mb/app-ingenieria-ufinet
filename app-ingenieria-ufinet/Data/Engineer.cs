using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class Engineer
{
    public int EngineerId { get; set; }

    public string? Name { get; set; }

    public string UserName { get; set; } = null!;

    public int? ActiveStatus { get; set; }

    public int? IdRegion { get; set; }

    public int EngineerTypeId { get; set; }

    public virtual EngineerType EngineerType { get; set; } = null!;

    public virtual Region? IdRegionNavigation { get; set; }

    public virtual ICollection<ServiceDeskTicket> ServiceDeskTicketAssigneeNavigations { get; set; } = new List<ServiceDeskTicket>();

    public virtual ICollection<ServiceDeskTicket> ServiceDeskTicketAssignerNavigations { get; set; } = new List<ServiceDeskTicket>();

    public virtual ICollection<ServiceDeskTicket> ServiceDeskTicketCreatorNavigations { get; set; } = new List<ServiceDeskTicket>();

    public virtual ICollection<ServiceDeskTicket> ServiceDeskTicketFinisherNavigations { get; set; } = new List<ServiceDeskTicket>();

    public virtual ICollection<ServiceDeskTicketLog> ServiceDeskTicketLogs { get; set; } = new List<ServiceDeskTicketLog>();

    public virtual ICollection<ServiceDeskTicketStatusHistory> ServiceDeskTicketStatusHistories { get; set; } = new List<ServiceDeskTicketStatusHistory>();

    public virtual Usuario UserNameNavigation { get; set; } = null!;
}
