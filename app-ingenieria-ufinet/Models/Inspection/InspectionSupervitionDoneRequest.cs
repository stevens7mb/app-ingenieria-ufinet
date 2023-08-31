namespace app_ingenieria_ufinet.Models.Inspection
{
    public class Pregunta
    {
        public int idCategoriaTarea { get; set; }
        public string idTarea { get; set; }
        public string idRespuesta { get; set; }
        public string observacion { get; set; }
    }

    public class Category
    {
        public string categoria { get; set; }
        public List<Pregunta> preguntas { get; set; }
    }

    public class InspectionSupervitionDoneRequest
    {
        public int idInspeccion { get; set; }
        public List<Category> categorias { get; set; }
    }
}
