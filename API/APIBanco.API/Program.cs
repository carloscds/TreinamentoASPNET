
using APIBanco.API;
using APIBanco.Core.Interfaces;
using APIBanco.Core.Services;
using APIBanco.Middlewares;

namespace APIBanco
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
            builder.Services.AddCustomServices(builder.Configuration);
            builder.Services.AddCustomSwagger();
            builder.Services.AddSecurity(builder.Configuration);
            builder.Services.AddScoped<IClienteService, ClienteService>();

            var app = builder.Build();
            app.UseCustomEndpoints();
            app.Run();
        }
    }
}
