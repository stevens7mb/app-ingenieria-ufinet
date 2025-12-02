namespace app_ingenieria_ufinet.Models.Indicadores.Factibilidad
{
    public class FactibilidadRequestModel
    {
        public int? IdFactibilidad { get; set; }
        public string? Estudio { get; set; }
        public string Ticket { get; set; }
        public int Cliente { get; set; }
        public int KAM { get; set; }
        public int BW { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime FechaRespuesta { get; set; }
        public int SitioConCobertura { get; set; }
        public int SitioConCoberturaParcial { get; set; }
        public int SitioSinCobertura { get; set; }
        public int IdTipoServicio { get; set; }
        public string? Coordinate { get; set; }
        public int? IdMunicipality { get; set; }
        public int? IdState { get; set; }
        public decimal? Capex { get; set; }
        public decimal? Opex { get; set; }
        public bool IsSubcontracted { get; set; }
    }
}
