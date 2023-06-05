namespace app_ingenieria_ufinet.Models.Inspection
{
    public class DetallePreguntasInspeccionModel
    {
        public int IdInspeccionTrabajoTarea { get; set; }
        public int IdTarea { get; set; }
        public string Tarea { get; set; }
        public int IdCategoriaTarea { get; set; }
        public string Categoria { get; set; }
        public int IdRespuesta { get; set; }
        public string Respuesta { get; set; }
    }
}
