namespace APIBanco.Domain.Models
{
    public class ClienteResponseDTO
    {
        public Guid Key { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Endereco { get; set; }
    }
}
