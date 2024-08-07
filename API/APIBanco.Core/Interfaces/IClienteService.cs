using APIBanco.Domain.Entidade;
using APIBanco.Domain.Models;

namespace APIBanco.Core.Interfaces
{
    public interface IClienteService
    {
        IList<ClienteResponseDTO> GetAll();
        ClienteResponseDTO GetById(int id);
        ClienteResponseDTO GetByKey(Guid key);
        Cliente GetByKeyDomain(Guid key);
        Guid Add(ClienteRequestDTO cliente);
        bool Update(ClienteRequestDTO cliente);
        bool Delete(Guid key);
        IList<ModelErrors> GetErrors();
    }
}
