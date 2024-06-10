using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class ServiceDeskTicket
{
    public int IdPrefix { get; set; }

    public int IdTicket { get; set; }

    public string TicketName { get; set; } = null!;

    public int Creator { get; set; }

    public int? Assigner { get; set; }

    public int? Assignee { get; set; }

    public string? Coordinate { get; set; }

    public int IdMunicipality { get; set; }

    public int IdState { get; set; }

    public int IdRegion { get; set; }

    public string Address { get; set; } = null!;

    public int IdIncidentType { get; set; }

    public int IdPrimaryCause { get; set; }

    public string? FaultDetail { get; set; }

    public int IdAffectedElement { get; set; }

    public int IdTicketState { get; set; }

    public int IdPriorityLevel { get; set; }

    public int? IdSolutionType { get; set; }

    public int? IsTechnicalVisitRequired { get; set; }

    public int? IsBrigadeDeployed { get; set; }

    public int? IsChangeNetworkTopology { get; set; }

    public string? WorkDone { get; set; }

    public int? IdConfirmedArea { get; set; }

    public string? IncidentClosureSummary { get; set; }

    public int? Finisher { get; set; }

    public virtual Engineer? AssigneeNavigation { get; set; }

    public virtual Engineer? AssignerNavigation { get; set; }

    public virtual Engineer CreatorNavigation { get; set; } = null!;

    public virtual Engineer? FinisherNavigation { get; set; }

    public virtual AffectedElement IdAffectedElementNavigation { get; set; } = null!;

    public virtual ConfirmedArea? IdConfirmedAreaNavigation { get; set; }

    public virtual IncidentType IdIncidentTypeNavigation { get; set; } = null!;

    public virtual Municipality IdMunicipalityNavigation { get; set; } = null!;

    public virtual Prefix IdPrefixNavigation { get; set; } = null!;

    public virtual PrimaryCause IdPrimaryCauseNavigation { get; set; } = null!;

    public virtual PriorityLevel IdPriorityLevelNavigation { get; set; } = null!;

    public virtual Region IdRegionNavigation { get; set; } = null!;

    public virtual SolutionType? IdSolutionTypeNavigation { get; set; }

    public virtual State IdStateNavigation { get; set; } = null!;

    public virtual TicketState IdTicketStateNavigation { get; set; } = null!;

    public virtual ICollection<ServiceDeskTicketFile> ServiceDeskTicketFiles { get; set; } = new List<ServiceDeskTicketFile>();

    public virtual ICollection<ServiceDeskTicketLog> ServiceDeskTicketLogs { get; set; } = new List<ServiceDeskTicketLog>();

    public virtual ICollection<ServiceDeskTicketStatusHistory> ServiceDeskTicketStatusHistories { get; set; } = new List<ServiceDeskTicketStatusHistory>();
}
