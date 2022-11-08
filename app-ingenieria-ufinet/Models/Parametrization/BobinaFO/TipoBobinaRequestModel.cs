namespace app_ingenieria_ufinet.Models.Parametrization.BobinaFO
{
    public class TipoBobinaRequestModel
    {
        public int? idTipoBobinaFO { get; set; }
        public string TipoBobinaFO { get; set; }
        public decimal Precio { get; set; }
        public int IdUnidadMedidaPrecio { get; set; }
        public int IdMoneda { get; set; }
        public decimal DistanciaBobina { get; set; }
        public int IdUnidadMedidaBobina { get; set; }
        public int? Estado { get; set; }
    }
}
