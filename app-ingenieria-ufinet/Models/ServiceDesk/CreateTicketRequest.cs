namespace app_ingenieria_ufinet.Models.ServiceDesk
{
    public class CreateTicketRequest
    {
        /// <summary>
        /// Id Prefix
        /// </summary>
        public required int PrefixId { get; set; }

        /// <summary>
        /// Ticket name
        /// </summary>
        public required string TicketName { get; set; } 

        /// <summary>
        /// UserCreate
        /// </summary>
        public required string UserCreate { get; set; }

        /// <summary>
        /// Coordinates gps
        /// </summary>
        public required string Coordinate { get; set; }

        /// <summary>
        /// Province Id
        /// </summary>
        public required int StateId { get; set; }

        /// <summary>
        /// Municipality Id
        /// </summary>
        public required int MunicipalityId { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        public required string Address { get; set; }

        /// <summary>
        /// Priority
        /// </summary>
        public required int PriorityId { get; set; }

        /// <summary>
        /// Maintance engineer Id
        /// </summary>
        public required int MaintanceEngineerId { get; set; }

        /// <summary>
        /// Incident type Id
        /// </summary>
        public required int IncidentTypeId { get; set; }

        /// <summary>
        /// Primary cause Id
        /// </summary>
        public required int PrimaryCauseId { get; set; }

        /// <summary>
        /// Affected element Id
        /// </summary>
        public required int AffectedElementId { get; set; }

        /// <summary>
        /// Summary ticket
        /// </summary>
        public required string SummaryTicket { get; set; }

        /// <summary>
        /// Files
        /// </summary>
        public required IFormFileCollection Files { get; set; }
    }
}
