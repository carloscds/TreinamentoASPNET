using APIBanco.Domain.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIBanco.InfraEstrutura.EF.Validation
{
    public class ClienteValidator : AbstractValidator<ClienteRequestDTO>
    {
        public ClienteValidator()
        {
            RuleFor(x => x.Nome).NotEmpty().WithMessage("Nome é obrigatório");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email é obrigatório");
            RuleFor(x => x.Endereco).NotEmpty().WithMessage("Endereço é obrigatório");
        }
    }
}
