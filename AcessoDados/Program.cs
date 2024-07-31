using System.Data.SqlClient;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;

namespace AcessoDados
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var conexao = new SqlConnection("data source=(local); initial catalog=oab; user id=teste; password=teste;");

            //conexao.Open();
            //var cmd = new SqlCommand("select * from cliente", conexao);
            //var reader = cmd.ExecuteReader();
            //while (reader.Read())
            //{
            //    var id = int.Parse(reader["Id"].ToString());
            //    var idObj = (object)id;
            //    System.Console.WriteLine($"{id} - {reader["Nome"]}");
            //}
            //reader.Close();
            //conexao.Close();


            //var dados = conexao.Query<Cliente>("select id,nome from cliente where i d > meuvalor", new {meuvalor = 10});
            //foreach (var cliente in dados)
            //{
            //    System.Console.WriteLine($"{cliente.Id} - {cliente.Nome}");
            //}

            //ORM - Object Relational Mapping (mapeamento objeto relacional)

            var dbOptions = new DbContextOptionsBuilder<Contexto>()
                                .UseSqlServer(conexao)
                                .LogTo(s => Console.WriteLine(s))
                                .Options;
            var db = new Contexto(dbOptions);

            var clientes = db.Cliente.Where(w => w.Id > 0);
            clientes = clientes.OrderByDescending(o => o.Nome)
                .Select(s => new Cliente { Nome = s.Nome });

            foreach (var cliente in clientes)
            {
                System.Console.WriteLine($"{cliente.Id} - {cliente.Nome}");
            }



        }
    }
}
