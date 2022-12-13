namespace app_ingenieria_ufinet.Models.PI
{
    public class DetallesPIResumenResponseModel
    {
        public string NombrePI { get; set; }
        public int NumPI { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime FechaOC { get; set; }
        public DateTime FechaBodegaSucursal { get; set; }
        public decimal TotalPIResumenCompra { get; set; }
        public decimal Incurrido { get; set; }
        public decimal PendienteIncurrir { get; set; }
        public decimal TotalPIFO { get; set; }
        public decimal TotalPIHerrajes { get; set; }
        public decimal TotalPI { get; set; }
        public string ComentarioPI { get; set; }
    }
}
