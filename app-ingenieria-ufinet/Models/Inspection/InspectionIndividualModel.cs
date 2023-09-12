namespace app_ingenieria_ufinet.Models.Inspection
{
    public class InspectionIndividualModel
    {
        public int IdInspeccionTrabajo { get; set; }
        public string IdServicioNuevo { get; set; }
        public string Contrata { get; set; }    
        public string Tecnico { get; set; }
        public string Ingeniero { get; set; }
        public DateTime FechaInspeccion { get; set; }
        public string UsuarioInspeccion { get; set; } 
        public string Estado { get; set; }
    }
}
