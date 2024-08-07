using Microsoft.AspNetCore.Identity;

namespace APIBanco.Domain.Entidade
{
    public class Usuario : IdentityUser
    {
        public string Nome { get; set; }
        public bool Ativo { get; set; }
    }
}
