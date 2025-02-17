using ControleGastosResidenciaisAPI.Domain.Models.PessoaModel;
using System.Text.Json.Serialization;

namespace ControleGastosResidenciaisAPI.Domain.Models.TransacaoModel
{
    public class Transacao
    {
        public Guid Identificador { get; set; } = Guid.NewGuid();
        public Guid PessoaIdentificador { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TipoTransacao Tipo { get; set; }
        public static List<Transacao> Transacoes { get; set; } = [];

        //Construtor para criação da instancia do objeto
        public Transacao(Guid pessoaIdentificador, string descricao, decimal valor, TipoTransacao tipo)
        {
            //Condicional para verificar se a pessoa associada a transação existe
            var pessoaIdentificada = Pessoa.BuscarPessoaID(pessoaIdentificador)
              ?? throw new ArgumentException("Pessoa não encontrada.");

            //Condicional para verificar a idade da pessoa e a partir disso permitir ou não ela ter uma receita
            if (pessoaIdentificada.Idade < 18 && tipo == TipoTransacao.Receita)
            {
                throw new InvalidOperationException("Menores de 18 anos só podem registrar despesas.");
            }

            if (valor <= 0)
            {
                throw new InvalidOperationException("O valor deve ser um número decimal positivo.");
            }

            PessoaIdentificador = pessoaIdentificador;
            Descricao = descricao;
            Valor = valor;
            Tipo = tipo;
            //Adicionar a transacao a lista estatica global de transacoes
            Transacoes.Add(this);
            //Adicionar a transacao a lista pessoal de transacoes
            pessoaIdentificada.AdicionarTransacao(this);
        }

        public static List<Transacao> ListarTransacoes()
        {
            return Transacoes;
        }
    }

    public enum TipoTransacao
    {
        Despesa,
        Receita
    }

}
