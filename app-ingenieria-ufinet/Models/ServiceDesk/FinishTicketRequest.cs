namespace app_ingenieria_ufinet.Models.ServiceDesk
{
    /// <summary>
    /// Request para finalización de ticket
    /// </summary>
    public class FinishTicketRequest
    {
        /// <summary>
        /// Prefijo del ticket
        /// </summary>
        public required int PrefixId { get; set; }

        /// <summary>
        /// Identificador del ticket
        /// </summary>
        public required int TicketId { get; set; }

        /// <summary>
        /// Usuario
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// Id de tipo de solucion
        /// </summary>
        public required int SolutionTypeId { get; set; }

        /// <summary>
        /// Requiere visita técnica?
        /// </summary>
        public required int IsTechnicalVisitRequired { get; set; }

        /// <summary>
        /// Requiere desplazamiento de brigada
        /// </summary>
        public required int IsBrigadeDeployed { get; set; }

        /// <summary>
        /// Requiere cambio de topologia de red
        /// </summary>
        public required int IsChangeNetworkTopology { get; set; }

        /// <summary>
        /// Confirmado por area
        /// </summary>
        public required int IdConfirmedArea { get; set; }

        /// <summary>
        /// Trabajo realizado
        /// </summary>
        public required string WorkDone { get; set; }

        /// <summary>
        /// Resumen de finalización de ticket
        /// </summary>
        public required string IncidentClosureSummary { get; set; }


        /// <summary>
        /// Files
        /// </summary>
        public IFormFileCollection? Files { get; set; }
    }
}