using APIBanco.Domain.Abstract;

namespace APIBanco.Domain.Entidade
{
    public class Cliente : EntityBase
    {
        public string Nome { get; set; }

        public string Email { get; set; }

        public string Endereco { get; set; }
    }
}
