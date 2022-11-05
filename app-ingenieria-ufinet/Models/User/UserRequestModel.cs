namespace app_ingenieria_ufinet.Models.User
{
    public class UserRequestModel
    {
        public string Usuario { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public int Sucursal { get; set; }
        public string Roles { get; set; }   
    }
}
