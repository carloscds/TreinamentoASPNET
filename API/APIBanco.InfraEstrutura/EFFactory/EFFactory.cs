using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using APIBanco.InfraEstrutura.Models;
using Microsoft.Extensions.Configuration;

namespace APIBanco.InfraEstrutura.EFFactory
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<OABContext>
    {
        public OABContext CreateDbContext(string[] args)
        {
            var diretorioPai = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            var diretorioJson = Path.Combine(diretorioPai, @"APIBanco.API");
            var configuration = new ConfigurationBuilder()
                .SetBasePath(diretorioJson)
                .AddJsonFile("appsettings.Development.json")
                .Build();
            var dbOptions = new DbContextOptionsBuilder<OABContext>()
                                .UseSqlServer(configuration.GetConnectionString("Banco"))
                                .Options;
            var db = new OABContext(dbOptions);
            return db;
        }
    }
}
