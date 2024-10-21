namespace ElimperioAPI.Models
{
    public class UserLoginRequest: User
    {
        public string Username { get; set; } = string.Empty;
        public string Contraseña { get; set; } = string.Empty;
    }
}
