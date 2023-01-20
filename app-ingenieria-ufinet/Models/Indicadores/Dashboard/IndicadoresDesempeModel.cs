namespace app_ingenieria_ufinet.Models.Indicadores.Dashboard
{
    public class IndicadoresDesempeModel
    {
        public int Correlativo { get; set; }
        public int SitiosTotales { get; set; }
        public string SitiosConCoberturaP { get; set; }
        public string SitiosConCoberturaParcialP { get; set; }
        public string SitiosSinCoberturaP { get; set; } 
        public int EstudiosTotales { get; set; }    
        public int TiempoPromedioEntrega { get; set; }
        public string Mes { get; set; }
    }
}
