using APIBanco.Core.Interfaces;
using APIBanco.Domain.DTO;
using APIBanco.Domain.Entidade;
using APIBanco.InfraEstrutura.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace APIBanco.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly ILogger<ClienteController> _logger;
        private IConfiguration _config;
        private readonly IClienteService _cliente;

        public ClienteController(ILogger<ClienteController> logger, IConfiguration config, IClienteService cliente)
        {
            _logger = logger;
            _config = config;
            _cliente = cliente;
        }

        [HttpPost]
        public IActionResult Adicionar(ClienteRequestDTO cliente)
        {
            var guid = _cliente.Add(cliente);
            if(guid == Guid.Empty)
            {
                return BadRequest(_cliente.GetErrors());
            }
            return Created("",cliente.Key);
        }

        [HttpGet]
        public IActionResult GetClienteDB()
        {
            var clientes = _cliente.GetAll();
            return Ok(clientes);
        }

        [HttpGet("{key}")]
        public IActionResult GetClienteDB(Guid key)
        {
            var cliente = _cliente.GetByKey(key); 
            if(cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }

        [HttpGet("domain/{key}")]
        public IActionResult GetClienteDomainDB(Guid key)
        {
            var clienteDomain = _cliente.GetByKeyDomain(key);
            if (clienteDomain == null)
            {
                return NotFound();
            }
            return Ok(clienteDomain.ToDTO());
        }

        [HttpPut]
        public IActionResult Atualizar([FromBody] ClienteRequestDTO cliente)
        {
            if (_cliente.Update(cliente))
            {
                return Ok();
            }
            else
            {
                return BadRequest(_cliente.GetErrors());
            }
        }

        [HttpDelete("{key}")]
        public IActionResult DeletarCliente(Guid key)
        {
            if(_cliente.Delete(key))
            {
                return Ok();
            }
            else
            {
                return BadRequest(_cliente.GetErrors());
            }
        }
    }
}
