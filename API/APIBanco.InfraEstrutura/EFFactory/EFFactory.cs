using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APIBanco.InfraEstrutura.Models;

namespace APIBanco.InfraEstrutura.EFFactory
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<OABContext>
    {
        public OABContext CreateDbContext(string[] args)
        {
            var conexao = new SqlConnection("data source=(local); initial catalog=oab; user id=teste; password=teste;trusted_connection=true; encrypt=false;");
            var dbOptions = new DbContextOptionsBuilder<OABContext>()
                                .UseSqlServer(conexao)
                                .Options;
            var db = new OABContext(dbOptions);
            return db;
        }
    }
}
