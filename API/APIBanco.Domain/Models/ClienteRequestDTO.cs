namespace APIBanco.Domain.Models
{
    public class ClienteRequestDTO
    {
        public Guid Key { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Endereco { get; set; }
    }
}
