namespace app_ingenieria_ufinet.Models.ServiceDesk
{
    /// <summary>
    /// Bitacora de ticket
    /// </summary>
    public class TicketBinnacle
    {
        /// <summary>
        /// Identificador del log registro
        /// </summary>
        public required int IdTicketLog { get; set; }

        /// <summary>
        /// Comentario
        /// </summary>
        public required string Comment { get; set; }

        /// <summary>
        /// Quien ingreso bitacora
        /// </summary>
        public required string CreatedBy { get; set;}

        /// <summary>
        /// Fecha registro
        /// </summary>
        public required DateTime CreatedDate { get; set;}
    }
}
