using APIBanco.Domain.Entidade;

namespace APIBanco.Domain.Extensions
{
    public static class ExtensionsDTO
    {
        public static object ToDTO(this Cliente cliente)
        {
            return new
            {
                cliente.Id,
                cliente.Nome,
                cliente.Endereco,
                cliente.Email
            };
        }
    }
}
