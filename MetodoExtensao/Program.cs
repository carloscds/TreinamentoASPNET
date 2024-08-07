namespace MetodoExtensao
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var cliente = new Cliente() { Id = 1, Nome = "Joao", Endereco = "Rua XPTO"};
            var dto = cliente.ToDto();

            var pro = new Produto() { Id = 1, Descricao = "Produto 1" };
            var dtoPro = pro.ToDto(2);

            var str = "este é um exemplo a ser convertido";
            var strConvertida = str.ConvertToCapital();

            var strValor = "10.50";
            var valor = strValor.ConvertoToDecimal();
        }
    }

    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }

        public void Add() { }
    }

    public class Produto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

        public void Calc() 
        {
            var codigoAtual = this.Id;
        }

        public Produto ProdutoAtual()
        {
            return this;
        }
    }


    public static class MinhasExtensoes
    {
        public static string ConverteString(this Cliente cliente)
        {
            return $"Id: {cliente.Id} - Nome: {cliente.Nome}";
        }

        public static object ToDto(this Cliente cliente)
        {
            return new { Codigo = cliente.Id, Nome = cliente.Nome, EnderecoResidencial = cliente.Endereco };
        }

        public static object ToDto(this Produto produto, int multiplicador)
        {
            return new { Codigo = produto.Id, Nome = produto.Descricao, Mult = multiplicador };
        }

        public static string ConvertToCapital(this string str)
        {
            return str.ToUpper();
        }

        public static decimal ConvertoToDecimal(this string valor)
        {
            decimal.TryParse(valor, out decimal valorDecimal);
            return valorDecimal;
        }

    }




}