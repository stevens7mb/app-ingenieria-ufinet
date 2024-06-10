namespace app_ingenieria_ufinet.Models.ServiceDesk
{
    /// <summary>
    /// Estado del ticket
    /// </summary>
    public class TicketStatus
    {
        /// <summary>
        /// Identificador de estado registro
        /// </summary>
        public required int IdTicketState { get; set; }

        /// <summary>
        /// Estado del ticket
        /// </summary>
        public required string TicketStatusDescription { get; set; }

        /// <summary>
        /// Quien hizo el cambio de estado
        /// </summary>
        public required string ChangedBy { get; set; }

        /// <summary>
        /// Fecha de cambio de estado
        /// </summary>
        public required DateTime ChangedDate { get; set; }
    }
}
