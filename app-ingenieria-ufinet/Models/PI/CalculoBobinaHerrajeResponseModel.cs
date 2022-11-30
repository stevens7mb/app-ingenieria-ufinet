namespace app_ingenieria_ufinet.Models.PI
{
    public class CalculoBobinaHerrajeResponseModel
    {
        public int IdTipoBobinaFO { get; set; }
        public decimal CantidadBobinas { get; set; }
        public decimal TotalBobina { get; set; }
        public decimal TotalHerrajes { get; set; }
        public List<CalculoHerrajesResponseModel> herrajes { get; set; }
    }
}
