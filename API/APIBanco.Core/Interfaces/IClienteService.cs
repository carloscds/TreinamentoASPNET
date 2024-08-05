using APIBanco.Domain.DTO;
using APIBanco.Domain.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIBanco.Core.Interfaces
{
    public interface IClienteService
    {
        IList<ClienteResponseDTO> GetAll();
        ClienteResponseDTO GetById(int id);
        ClienteResponseDTO GetByKey(Guid key);
        Guid Add(ClienteRequestDTO cliente);
        bool Update(ClienteRequestDTO cliente);
        bool Delete(Guid key);
        IList<ModelErrors> GetErrors();
    }
}
