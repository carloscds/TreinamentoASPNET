using WebApplication4.Domain;
using WebApplication4.DTO;

namespace WebApplication4.Interface
{
    public interface IClienteService
    {
        ModelError GetErro();
        bool Add(ClienteDTO cliente);
        bool Update(ClienteDTO cliente);
        bool Delete(int id);
        bool Ativar(int id, bool ativo);
        ClienteDTO Get(int id);
    }
}
