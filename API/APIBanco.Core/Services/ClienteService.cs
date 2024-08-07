using APIBanco.Core.Abstract;
using APIBanco.Core.Interfaces;
using APIBanco.Domain.Entidade;
using APIBanco.Domain.Models;
using APIBanco.InfraEstrutura.Repository;

namespace APIBanco.Core.Services
{
    public class ClienteService : ServiceBase<ClienteRequestDTO>, IClienteService
    {
        private readonly IRepositoryBase<Cliente> _cliente;

        public ClienteService(IRepositoryBase<Cliente> cliente)
        {
            _cliente = cliente;
        }

        public Guid Add(ClienteRequestDTO cliente)
        {
            if(!ValidateModel(cliente))
            { 
                return Guid.Empty;
            }
            var clienteAdd = new Cliente
            {
                Nome = cliente.Nome,
                Email = cliente.Email,
                Endereco = cliente.Endereco
            };
            _cliente.Add(clienteAdd);
            return clienteAdd.Key;
        }

        public bool Delete(Guid key)
        {
            var cliente = _cliente.GetByPredicate(w => w.Key == key);
            if(cliente == null)
            {
                _errors.Add(new ModelErrors { Mensagem = "Cliente não encontrado" });
                return false;
            }
            _cliente.Delete(cliente);
            return true;
        }

        public IList<ClienteResponseDTO> GetAll()
        {
            return _cliente.GetAll().Select(w => new ClienteResponseDTO
            {
                Key = w.Key,
                Nome = w.Nome,
                Email = w.Email,
                Endereco = w.Endereco
            }).ToList();
        }

        public ClienteResponseDTO GetById(int id)
        {
            var cliente = _cliente.GetID(id);
            return new ClienteResponseDTO
            {
                Key = cliente.Key,
                Nome = cliente.Nome,
                Email = cliente.Email,
                Endereco = cliente.Endereco
            };
        }

        public ClienteResponseDTO GetByKey(Guid key)
        {
            var cliente = _cliente.GetByPredicate(w => w.Key == key);
            return new ClienteResponseDTO
            {
                Key = cliente.Key,
                Nome = cliente.Nome,
                Email = cliente.Email,
                Endereco = cliente.Endereco
            };
        }

        public Cliente GetByKeyDomain(Guid key)
        {
            return _cliente.GetByPredicate(w => w.Key == key);
        }

        public bool Update(ClienteRequestDTO cliente)
        {
            var clienteUpdate = _cliente.GetByPredicate(w => w.Key == cliente.Key);
            if (clienteUpdate == null)
            {
                _errors.Add(new ModelErrors { Mensagem = "Cliente não encontrado" });
                return false;
            }
            clienteUpdate.Nome = cliente.Nome;
            clienteUpdate.Email = cliente.Email;
            clienteUpdate.Endereco = cliente.Endereco;
            _cliente.Update(clienteUpdate);
            return true;
        }
    }
}
