using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIBanco.Domain.DTO
{
    public class ClienteResponseDTO
    {
        public Guid Key { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Endereco { get; set; }
    }
}
