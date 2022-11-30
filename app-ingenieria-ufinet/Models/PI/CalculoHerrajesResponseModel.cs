namespace app_ingenieria_ufinet.Models.PI
{
    public class CalculoHerrajesResponseModel
    {
        public int IdTipoHerraje { get; set; }
        public string TipoHerraje { get; set; }
        public decimal Precio { get; set; }
        public string Formula { get; set; }
        public decimal FormulaOut { get; set; }
    }
}
