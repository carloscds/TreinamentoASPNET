using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using WebApplication4.Domain;
using WebApplication4.DTO;
using WebApplication4.Interface;

namespace WebApplication4.Service
{
    public class ClienteService : IClienteService
    {
        private ModelError MensagemErro;

        public ModelError GetErro() => MensagemErro;

        public bool Add(ClienteDTO cliente)
        {
            return true;
        }

        public bool Delete(int id)
        {
            return true;
        }

        public ClienteDTO Get(int id)
        {
            return new ClienteDTO { Id = 1, Nome = "Cliente", Email = "teste@teste.com" };
        }

        public bool Update(ClienteDTO cliente)
        {
            if (cliente.Id == 0)
            {
                MensagemErro = new ModelError { Code = 400, Message = "Id do cliente não informado." };
                return false;
            }

            Cliente retornoBanco = null;
            if (retornoBanco == null)
            {
                MensagemErro = new ModelError { Code = 404, Message = "Id do cliente não existe." };
                return false;
            }
            // update do dado
            return true;
        }

        public bool Ativar(int id, bool ativo)
        {
            if (id == 0)
            {
                MensagemErro = new ModelError { Code = 400, Message = "Id do cliente não informado." };
                return false;
            }

            //Cliente retornoBanco = null;
            //if (retornoBanco == null)
            //{
            //    MensagemErro = "Id do cliente não existe";
            //    return false;
            //}
            // update do dado
            return true;
        }
    }
}
