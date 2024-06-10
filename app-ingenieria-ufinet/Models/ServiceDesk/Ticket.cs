namespace app_ingenieria_ufinet.Models.ServiceDesk
{
    public class Ticket
    {
        /// <summary>
        /// Prefijo del ticket
        /// </summary>
        public required int PrefixId { get; set; }

        /// <summary>
        /// Id del ticket
        /// </summary>
        public required int TicketId { get; set; }

        /// <summary>
        /// Código del ticket, union de prefix y ticket
        /// </summary>
        public required string TicketCode { get; set; }

        /// <summary>
        /// Nombre del ticket
        /// </summary>
        public required string TicketName { get; set; }

        /// <summary>
        /// Estado del ticket
        /// </summary>
        public required string TicketStatus { get; set; }

        /// <summary>
        /// Prioridad
        /// </summary>
        public required string Priority { get; set; }

        /// <summary>
        /// Creado por
        /// </summary>
        public required string CreatedBy { get; set; }

        /// <summary>
        /// Fecha de creación
        /// </summary>
        public required DateTime CreatedDate { get; set; }  

        /// <summary>
        /// Asignado a
        /// </summary>
        public string? AssigneeBy { get; set; }

        /// <summary>
        /// Asignado por
        /// </summary>
        public string? AssigneeFor { get; set; }

        /// <summary>
        /// Coordenadas
        /// </summary>
        public required string Coordinate { get; set; }

        /// <summary>
        /// Municipio
        /// </summary>
        public required string Municipality { get; set; }

        /// <summary>
        /// Departamento
        /// </summary>
        public required string State { get; set; }

        /// <summary>
        /// Region
        /// </summary>
        public required string Region { get; set; }

        /// <summary>
        /// Dirección
        /// </summary>
        public required string Address { get; set; }

        /// <summary>
        /// Tipo de incidente
        /// </summary>
        public required string IncidentType { get; set; }

        /// <summary>
        /// Causa primaria
        /// </summary>
        public required string PrimaryCause { get; set; }

        /// <summary>
        /// Elemento afectado
        /// </summary>
        public required string AffectedElement { get; set; }

        /// <summary>
        /// Detalles de la falla
        /// </summary>
        public required string FaultDetail { get; set; }

        /// <summary>
        /// Archivos asociados al ticket
        /// </summary>
        public List<TicketFile>? Files { get; set; }

        /// <summary>
        /// Bitacora 
        /// </summary>
        public List<TicketBinnacle>? Binnacles { get; set;}

        /// <summary>
        /// Estados del ticket
        /// </summary>
        public required List<TicketStatus> Statuses { get; set; }
    }
}
