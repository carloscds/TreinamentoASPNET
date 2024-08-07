using APIBanco.Domain.Entidade;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIBanco.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TesteController : ControllerBase
    {
        private readonly ILogger<ClienteController> _logger;
        private IConfiguration _config;

        public TesteController(ILogger<ClienteController> logger, IConfiguration config)
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
