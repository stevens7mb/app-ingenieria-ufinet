namespace app_ingenieria_ufinet.Models.PI
{
    public class PIResumenCompraRequest
    {
        public int numPI { get; set; }
        public string nombrePI { get; set; }    
        public string comentarioPI { get; set; }
        public DateTime fechaSolicitud { get; set; }
        public DateTime fechaOC { get; set; }
        public DateTime fechaBodegaSucursal { get; set; }
        public int idSucursal { get; set; }
        public decimal totalPIResumenCompra { get; set; }
        public string usuario { get; set; }
    }
}
