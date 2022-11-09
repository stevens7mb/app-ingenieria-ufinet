namespace app_ingenieria_ufinet.Models.Parametrization.Herraje
{
    public class TipoHerrajeRequestModel
    {
        public int? idTipoHerraje { get; set; }
        public string tipoHerraje { get; set; }
        public decimal precio { get; set; }
        public int idMoneda { get; set; }   
        public int idTipoBobinaFO { get; set; }
    }
}
