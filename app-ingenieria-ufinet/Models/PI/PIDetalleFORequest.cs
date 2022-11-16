namespace app_ingenieria_ufinet.Models.PI
{
    public class PIDetalleFORequest
    {
        public int idPIResumenCompra { get; set; }
        public int numPI { get; set; }
        public string nombrePI { get; set; }
        public DateTime fechaSolicitud { get; set; }
        public DateTime fechaOC { get; set; }
        public DateTime fechaBodegaSucursal { get; set; }
        public int idSucursal { get; set; }
        public decimal totalDetalleFOPIDesc { get; set; }
        public decimal incurrido { get; set; }
        public int idTipoBobinaFO { get; set; }
        public decimal distanciaPIDetalle { get; set; }
        public decimal cantidadBobinas { get; set; }    
        public decimal precioUnitario { get; set; }
        public decimal totalDetalleFOPI { get; set; }
        public string usuario { get; set; }
        public List<PIDetalleHerrajeRequest> herrajes { get; set; }
    }
}
