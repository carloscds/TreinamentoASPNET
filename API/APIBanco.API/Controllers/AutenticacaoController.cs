using API.Services.Controllers;
using APIBanco.API.Models;
using APIBanco.API.Services;
using APIBanco.Core.Abstract;
using APIBanco.Domain.Entidade;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace APIBanco.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AutenticacaoController : ControllerBaseAPI
    {
        private readonly ILogger<AutenticacaoController> _logger;
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly ITokenService _tokenService;

        public AutenticacaoController(UserManager<Usuario> userManager,
                                      SignInManager<Usuario> signInManager,
                                      ITokenService tokenService,
                                      IConfiguration config,
                                      ILogger<AutenticacaoController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _logger = logger;
        }

        // Nao deixar aberto - somente para fins ditaticos está aberto!
        [AllowAnonymous]
        [HttpPost("CriarUsuario")]
        public async Task<IActionResult> CriarUsuario([FromBody] UsuarioCreateDTO usuario)
        {
            if (usuario.Senha != usuario.ConfirmaSenha)
            {
                return BadRequest("Senhas não conferem");
            }
            var user = new Usuario
            {
                Nome = usuario.Nome,
                UserName = usuario.Email,
                Email = usuario.Email,
                EmailConfirmed = true,
                Ativo = true,
            };

            var usuarioService = new ServiceBase<Usuario>();
            if(!usuarioService.ValidateModel(user))
            {
                return BadRequest(usuarioService.GetErrors());
            }
            if (await _userManager.FindByEmailAsync(usuario.Email) != null)
            {
                return BadRequest("Email já existe");
            }
            var result = await _userManager.CreateAsync(user, usuario.Senha);

            if (result.Succeeded)
            {
                return Ok(user.Id);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDTO login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user == null)
            {
                return BadRequest("Usuário/Senha inválidos");
            }
            if (!user.Ativo)
            {
                return BadRequest("Usuário inativo");
            }
            var result = await _signInManager.PasswordSignInAsync(user, login.Senha, false, true);
            if (result.Succeeded)
            {
                // exemplo: atualizar data do último login
                await _userManager.UpdateAsync(user);
                return Ok(await _tokenService.GenerateJWT(user.Email));
            }
            else
            {
                return BadRequest("Usuário/Senha inválidos");
            }
        }

        [HttpPut("TrocarSenha")]
        public async Task<IActionResult> TrocarSenha([FromBody] UsuarioChangePasswordDTO changePassword)
        {
            var user = await _userManager.FindByIdAsync(LoggedUser);
            var passToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, passToken, changePassword.NovaSenha);

            if (result.Succeeded)
            {
                return Ok("Senha alterada com sucesso");
            }
            else
            {
                return BadRequest(result.Errors.ToList());
            }
        }
    }
}
