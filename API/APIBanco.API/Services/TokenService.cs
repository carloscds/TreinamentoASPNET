using APIBanco.API.Models;
using APIBanco.Domain.Entidade;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NetDevPack.Security.Jwt.Core.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace APIBanco.API.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly IJwtService _jwksService;
        private readonly IHttpContextAccessor _context;

        public TokenService(UserManager<Usuario> userManager,
                            IJwtService jwksService,
                            IHttpContextAccessor context)
        {
            _userManager = userManager;
            _jwksService = jwksService;
            _context = context;
        }

        public async Task<AuthJwtResponse> GenerateJWT(string email)
        {
            IList<Claim> claims;
            var user = await _userManager.FindByEmailAsync(email);
            var userId = user.Id;
            var userName = user.Nome;
            claims = await _userManager.GetClaimsAsync(user);
            var identityClaims = GetUserClaims(claims, userId);
            var encodedToken = await EncodeToken(identityClaims);
            return GetTokenResponse(encodedToken, userName, claims);
        }

        private ClaimsIdentity GetUserClaims(ICollection<Claim> claims, string UserId)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, UserId.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));
            claims.Add(new Claim("role", "user"));
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);
            return identityClaims;
        }

        private AuthJwtResponse GetTokenResponse(string encodedToken, string usuarioNome, IEnumerable<Claim> claims)
        {
            return new AuthJwtResponse
            {
                AccessToken = encodedToken,
                UserName = usuarioNome,
                ExpiresIn = (int)TimeSpan.FromHours(6).TotalSeconds
            };
        }

        private async Task<string> EncodeToken(ClaimsIdentity identityClaims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var currentIssuer = $"{_context.HttpContext.Request.Scheme}://{_context.HttpContext.Request.Host}";
            var key = await _jwksService.GetCurrentSigningCredentials();
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = currentIssuer,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = key
            });
            return tokenHandler.WriteToken(token);
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

    }
}
