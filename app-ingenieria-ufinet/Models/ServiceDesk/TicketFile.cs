namespace app_ingenieria_ufinet.Models.ServiceDesk
{
    public class TicketFile
    {
        /// <summary>
        /// Identificador de archivo
        /// </summary>
        public required int TicketFileId { get; set; }

        /// <summary>
        /// Nombre de archivo
        /// </summary>
        public required string NameFile { get; set; }

        /// <summary>
        /// Ruta archivo
        /// </summary>
        public required string Path { get; set;}
    }
}
