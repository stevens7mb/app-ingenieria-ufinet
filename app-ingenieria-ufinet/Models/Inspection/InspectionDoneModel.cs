namespace app_ingenieria_ufinet.Models.Inspection
{
    public class InspectionDoneModel
    {
        public InspectionIndividualModel datosIndividuales { get; set; }
        public List<CategoriaModel> categorias { get; set; }    
    }
}
