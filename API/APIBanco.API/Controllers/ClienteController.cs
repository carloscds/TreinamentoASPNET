using APIBanco.Domain.Entidade;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace APIBanco.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly ILogger<ClienteController> _logger;
        private IConfiguration _config;

        public ClienteController(ILogger<ClienteController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var conexaoBanco = _config.GetConnectionString("Banco");
            var conexaoBancoNoSQL = _config.GetConnectionString("BancoNoSQL");

            var smtp = _config.GetSection("ConfiguracaoEmail:SMTP").Value;
            var secao = _config.GetSection("ConfiguracaoEmail");

            var hashSenha = _config.GetSection("HashSenha").Value;

            return Ok(hashSenha);
        }

        [HttpGet("key/{key}")]
        public IActionResult GetKey(Guid key)
        {
            return Ok(key);
        }

        [HttpPost]
        public IActionResult Post()
        {
            var cliente = new Cliente();
            if(cliente == null)
            {
                return BadRequest("Cliente não informado");
            }

            return Ok();
        }
    }
}
