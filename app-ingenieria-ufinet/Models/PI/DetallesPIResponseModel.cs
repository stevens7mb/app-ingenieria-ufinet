namespace app_ingenieria_ufinet.Models.PI
{
    public class DetallesPIResponseModel
    {
        public DetallesPIResumenResponseModel detallesResumenPI { get; set; }
        public List<DetallesPIFOResponseModel> detallesFO { get; set; }
    }
}
