using app_ingenieria_ufinet.Models.Commons;

namespace app_ingenieria_ufinet.Models.PI
{
    public class CrearPIResponseModel
    {
        public int? idResumenCompra { get; set; }
        public string result { get; set; }
        public int numPI { get; set; }
        public List<SPResponseGenericWithVal> responseFO { get; set; }
        public List<SPResponseGenericWithVal> responseHerraje { get; set; }
    }
}
