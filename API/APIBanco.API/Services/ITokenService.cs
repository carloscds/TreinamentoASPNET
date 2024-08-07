using APIBanco.API.Models;

namespace APIBanco.API.Services
{
    public interface ITokenService
    {
        Task<AuthJwtResponse> GenerateJWT(string email);
    }
}
