
using APIBanco.Core.Interfaces;
using APIBanco.Core.Services;
using APIBanco.InfraEstrutura.Models;
using APIBanco.InfraEstrutura.Repository;
using APIBanco.Middlewares;
using Microsoft.EntityFrameworkCore;

namespace APIBanco
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            builder.Services.AddDbContext<OABContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Banco")));

            builder.Services.AddScoped<IClienteService, ClienteService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
            app.UseMiddleware<MiddlewareException>();
            app.UseMiddleware<MiddlewareToken>();
            app.Run();
        }
    }
}
