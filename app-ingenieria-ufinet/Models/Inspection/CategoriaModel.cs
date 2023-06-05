namespace app_ingenieria_ufinet.Models.Inspection
{
    public class CategoriaModel
    {
        public int IdCategoriaTarea { get; set; }
        public string Categoria { get; set; }
        public List<DetallePreguntasInspeccionModel> preguntas { get; set; }
    }
}
