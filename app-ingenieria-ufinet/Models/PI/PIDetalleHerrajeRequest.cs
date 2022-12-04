namespace app_ingenieria_ufinet.Models.PI
{
    public class PIDetalleHerrajeRequest
    {
        public int idTipoHerraje { get; set;} 
        public int idPIDetalleFO { get; set;}
        public int numPI { get; set; }
        public string nombrePI { get; set; }    
        public DateTime fechaSolicitud { get; set; }
        public DateTime fechaOC { get; set; }
        public DateTime fechaBodegaSucursal { get; set; }
        public int idSucursal { get; set; }
        public decimal totalDetalleFOPIDesc { get; set; }
        public decimal incurrido { get; set; }
        public decimal pendienteIncurrir { get; set; }  
        public decimal cantidadHerrajes { get; set; }
        public decimal precioUnitarioHerraje { get; set; }
        public decimal totalDetalleCompraHerraje { get; set; }
        public string usuario { get; set; }
    }
}
