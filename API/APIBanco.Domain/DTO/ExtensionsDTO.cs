using APIBanco.Domain.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIBanco.Domain.DTO
{
    public static class ExtensionsDTO
    {
        public static object ToDTO(this Cliente cliente)
        {
            return new 
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Endereco = cliente.Endereco,
                Email = cliente.Email
            };
        }
    }
}
