using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace AcessoDados
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<Contexto>
    {
        public Contexto CreateDbContext(string[] args)
        {
            var conexao = new SqlConnection("data source=(local); initial catalog=oab; user id=teste; password=teste;");
            var dbOptions = new DbContextOptionsBuilder<Contexto>()
                                .UseSqlServer(conexao)
                                .Options;
            var db = new Contexto(dbOptions);
            return db;
        }
    }
}
