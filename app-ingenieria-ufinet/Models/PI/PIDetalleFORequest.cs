namespace app_ingenieria_ufinet.Models.PI
{
    public class PIDetalleFORequest
    {
        public int? idPIResumenCompra { get; set; }
        public int idTipoBobinaFO { get; set; }
        public string tipoBobinaFO { get; set; }
        public decimal distanciaPIDetalle { get; set; }
        public decimal distanciaPIBobina { get; set; }
        public decimal cantidadBobinas { get; set; }    
        public decimal precioUnitario { get; set; }
        public decimal totalDetalleFOPI { get; set; }
        public decimal totalHerrajes { get; set; }
        public List<PIDetalleHerrajeRequest> herrajes { get; set; }
    }
}
