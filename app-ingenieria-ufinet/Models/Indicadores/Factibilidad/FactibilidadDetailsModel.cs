namespace app_ingenieria_ufinet.Models.Indicadores.Factibilidad
{
    public class FactibilidadDetailsModel
    {
        public int IdFactibilidad { get; set; }
        public string Ticket { get; set; }
        public string? Estudio { get; set; }
        public int IdCliente { get; set; }
        public int IdKAM { get; set; }
        public int BW { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime FechaRespuesta { get; set; }
        public int Estado { get; set; }
        public int SitioConCobertura { get; set; }
        public int SitioConCoberturaParcial { get; set; }
        public int SitioSinCobertura { get; set; }
        public int IdIngeniero { get; set; }
        public int IdTipoServicio { get; set; }
        public string Coordenada { get; set; }
        public int? IdMunicipio { get; set; }
        public int? IdDepartamento { get; set; }
        public decimal? Capex { get; set; }
        public decimal? Opex { get; set; }
    }
}
