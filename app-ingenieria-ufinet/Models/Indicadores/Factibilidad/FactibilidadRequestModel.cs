namespace app_ingenieria_ufinet.Models.Indicadores.Factibilidad
{
    public class FactibilidadRequestModel
    {
        public int? IdFactibilidad { get; set; }
        public string? Estudio { get; set; }
        public int Ticket { get; set; }
        public int Cliente { get; set; }
        public int KAM { get; set; }
        public int BW { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime FechaRespuesta { get; set; }
        public int SitioConCobertura { get; set; }
        public int SitioConCoberturaParcial { get; set; }
        public int SitioSinCobertura { get; set; }
        public int IdTipoServicio { get; set; }
    }
}
