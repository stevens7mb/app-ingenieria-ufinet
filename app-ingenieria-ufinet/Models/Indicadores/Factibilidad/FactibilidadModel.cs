namespace app_ingenieria_ufinet.Models.Indicadores.Factibilidad
{
    public class FactibilidadModel
    {
        public int IdFactibilidad { get; set; }
        public int Ticket { get; set; }
        public string Estudio { get; set; }
        public string Cliente { get; set; }
        public string KAM { get; set; }
        public int BW { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime FechaRespuesta { get; set; }
        public string Estado { get; set; }
        public int SitioConCobertura { get; set; }
        public int SitioConCoberturaParcial { get; set; }
        public int SitioSinCobertura { get; set; }
        public int SitiosAnalizados { get; set; }
        public string Ingeniero { get; set; }
        public string TipoServicio { get; set; }
    }
}
