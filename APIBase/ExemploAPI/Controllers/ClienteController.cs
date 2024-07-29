using Microsoft.AspNetCore.Mvc;
using WebApplication4.Domain;
using WebApplication4.DTO;
using WebApplication4.Interface;
using WebApplication4.Service;

namespace WebApplication4.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly ILogger<ClienteController> _logger;
        private readonly IClienteService _clienteService;

        public ClienteController(ILogger<ClienteController> logger, 
                                 IClienteService clienteService)
        {
            _logger = logger;
            _clienteService = clienteService;
        }

        [HttpGet("RetornaClientes")]
        [ProducesResponseType<Cliente>(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string),StatusCodes.Status400BadRequest)]
        public IActionResult Get()
        {
            return Ok(_clienteService.Get(1));  
        }

        [HttpPost("AdicionarCliente")]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult AdicionarCliente([FromBody] ClienteDTO cliente)
        {
            return Ok(_clienteService.Add(cliente));
        }

        [HttpPut("AlterarCliente")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult AlterarCliente([FromBody] ClienteDTO cliente)
        {
            var result = _clienteService.Update(cliente);
            if (result)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(_clienteService.GetErro());
            }
        }

        [HttpDelete("DeletarCliente/{id}")]
        public IActionResult DeletarCliente(int id)
        {
            var result = _clienteService.Delete(id);
            if (result)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(_clienteService.GetErro());
            }
        }

        [HttpPatch("AtivarCliente/{id}/{ativo}")]
        public IActionResult AtivarCliente(int id, bool ativo)
        {
            var result = _clienteService.Ativar(id, ativo);
            if (result)
            {
                return Ok($"Cliente {id} - Status: {ativo}");
            }
            else
            {
                return BadRequest(_clienteService.GetErro());
            }
        }

    }
}
