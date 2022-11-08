namespace app_ingenieria_ufinet.Models.Parametrization.BobinFO
{
    public class TipoBobinaFO
    {
        public int idTipoBobinaFO { get; set; }
        public string TipoBobinaFODesc { get; set; }
        public decimal Precio { get; set; }
        public string Simbolo { get; set; }
        public string UnidadMedidaPrecio { get; set; }
        public decimal DistanciaBobina { get; set; }
        public string UnidadMedidaBobina { get; set; }
        public string Estado { get; set; }
    }
}
