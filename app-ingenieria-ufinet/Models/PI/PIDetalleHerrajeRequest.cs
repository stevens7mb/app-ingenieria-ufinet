namespace app_ingenieria_ufinet.Models.PI
{
    public class PIDetalleHerrajeRequest
    {
        public int idTipoHerraje { get; set;} 
        public int? idPIDetalleFO { get; set;}
        public decimal cantidadHerrajes { get; set; }
        public decimal precioUnitarioHerraje { get; set; }
    }
}
