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

        /// <summary>
        /// Visita técncia requerida -1 SI, 0 NO
        /// </summary>
        public int? IsTechnicalVisitRequired { get; set; }

        /// <summary>
        /// Desplazamiento de brigada -1 SI, 0 NO
        /// </summary>
        public int? IsBrigadeDeployed { get; set; }

        /// <summary>
        /// Cambio de topología de red -1 SI, 0 NO
        /// </summary>
        public int? IsChangeNetworkTopology {get; set; }

        /// <summary>
        /// Tipo de solución id
        /// </summary>
        public int? IdSolutionType { get; set; }

        /// <summary>
        /// Tipo de solución
        /// </summary>
        public string? SolutionType { get; set; }

        /// <summary>
        /// Confirmado por id
        /// </summary>
        public int? IdConfirmedArea { get; set; }

        /// <summary>
        /// Confirmado por
        /// </summary>
        public string? ConfirmedArea { get; set; }

        /// <summary>
        /// Trabajos realizados
        /// </summary>
        public string? WorkDone { get; set; }

        /// <summary>
        /// Resumen de la incidencia cierre
        /// </summary>
        public string? IncidentClosureSummary { get; set; }

        /// <summary>
        /// Indica si el ticket se encuentra finalizado
        /// </summary>
        public bool IsTicketFinalizedStatus { get; set; }
    }
}
