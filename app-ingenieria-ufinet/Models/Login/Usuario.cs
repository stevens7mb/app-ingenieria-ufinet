namespace app_ingenieria_ufinet.Models.Login
{
    public class Usuario
    {
        public int Correlative { get; set; }
        public string User { get; set; }
        public string Contraseña { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public int idSucursal { get; set; }
        public int Activo { get; set; }
    }
}
