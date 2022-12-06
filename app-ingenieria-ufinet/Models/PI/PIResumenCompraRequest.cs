namespace app_ingenieria_ufinet.Models.PI
{
    public class PIResumenCompraRequest
    {
        public int numPI { get; set; }
        public string nombrePI { get; set; }    
        public DateTime fechaSolicitud { get; set; }
        public DateTime fechaOC { get; set; }
        public DateTime fechaBodegaSucursal { get; set; }
        public decimal totalPIEncabezado { get; set; }
        public decimal incurrido { get; set; }
        public decimal pendienteIncurrir { get; set; }
        public decimal totalPIFO { get; set; }
        public decimal totalPIHerrajes { get; set; }
        public decimal totalPI { get; set; }
        public string comentarioPI { get; set; }
    }
}
