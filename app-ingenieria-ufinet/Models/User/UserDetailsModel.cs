namespace app_ingenieria_ufinet.Models.User
{
    public class UserDetailsModel
    {
        public string Usuario { get; set; }
        public string Nombre { get; set; }
        public string Contraseña { get; set; }
        public string Email { get; set; }
        public int? idSucursal { get; set; }
        public string Roles { get; set; }
    }
}
