namespace app_ingenieria_ufinet.Models.Commons
{
    public class SPResponseGeneric
    {
        public int? FilasAfectadas { get; set; }    
        public string Mensaje { get; set; }
        public string Tipo { get; set; }
    }
    public class SPResponseGenericWithVal
    {
        public int? FilasAfectadas { get; set; }
        public string Mensaje { get; set; }
        public string Tipo { get; set; }
        public int? Id { get; set; }
    }
}
