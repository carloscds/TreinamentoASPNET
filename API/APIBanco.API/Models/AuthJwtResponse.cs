namespace APIBanco.API.Models
{
    public class AuthJwtResponse
    {
        public string AccessToken { get; set; }
        public string UserName { get; set; }
        public int ExpiresIn { get; set; }
    }
}
