namespace app_ingenieria_ufinet.Models.Indicadores.Factibilidad
{
    public class FactibilidadModel
    {
        public int IdFactibilidad { get; set; }
        public string Ticket { get; set; }
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
        public string Coordenada { get; set; }
        public int? IdMunicipio { get; set; }
        public string Municipio { get; set; }
        public int? IdDepartamento { get; set; }
        public string Departamento { get; set; }
        public decimal? Capex { get; set; }
        public decimal? Opex { get; set; }
        public string EstadoFactibilidad { get; set; }
    }
}
