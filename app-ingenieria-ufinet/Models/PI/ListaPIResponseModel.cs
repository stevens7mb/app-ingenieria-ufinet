namespace app_ingenieria_ufinet.Models.PI
{
    public class ListaPIResponseModel
    {
        public int IdPIResumenCompra { get; set; }
        public int NumPI { get; set; }
        public string NombrePI { get; set; }
        public DateTime? FechaGeneracion { get; set; }
        public string EstadoPI { get; set; }
        public decimal TotalPI { get; set; }
    }
}
