using APIBanco.Domain.Entidade;
using FluentValidation;

namespace APIBanco.InfraEstrutura.EF.Validation
{
    public class UsuarioValidator : AbstractValidator<Usuario>
    {
        public UsuarioValidator()
        {
            RuleFor(x => x.Nome).NotEmpty().WithMessage("Nome é obrigatório");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email é obrigatório");
        }
    }
}
