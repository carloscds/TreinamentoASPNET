using APIBanco.Domain.Models;
using FluentValidation;

namespace APIBanco.InfraEstrutura.EF.Validation
{
    public class ClienteRequestDTOValidator : AbstractValidator<ClienteRequestDTO>
    {
        public ClienteRequestDTOValidator()
        {
            RuleFor(x => x.Nome).NotEmpty().WithMessage("Nome é obrigatório");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email é obrigatório");
            RuleFor(x => x.Endereco).NotEmpty().WithMessage("Endereço é obrigatório");
        }
    }
}
