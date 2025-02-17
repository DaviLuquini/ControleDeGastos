using ControleGastosResidenciaisAPI.Domain.Models.TransacaoModel;

namespace ControleGastosResidenciaisAPI.Domain.Models.PessoaModel
{
    public class Pessoa
    {
        public string Nome { get; set; }
        public Guid Identificador { get; set; } = Guid.NewGuid();
        public int Idade { get; set; }
        //Criação de uma lista estatica global de pessoas
        public static List<Pessoa> Pessoas { get; set; } = [];
        //Criação de uma lista individual de transações
        public List<Transacao> Transacoes { get; set; } = [];

        //Construtor para criação da instancia do objeto
        public Pessoa(string nome, int idade)
        {
            Nome = nome;
            Idade = idade;

            //Adicionar a instancia a lista estatica global de pessoas
            Pessoas.Add(this);
        }

        public static Pessoa BuscarPessoaID(Guid identificador)
        {
            for (int i = 0; i < Pessoas.Count; i++)
            {
                if (identificador == Pessoas[i].Identificador)
                {
                    return Pessoas[i];
                }
            }
            return null;
        }

        public static Pessoa BuscarPessoaNome(string nome)
        {
            for (int i = 0; i < Pessoas.Count; i++)
            {
                if (nome == Pessoas[i].Nome)
                {
                    return Pessoas[i];
                }
            }
            return null;
        }

        public static List<Pessoa> ListarPessoas()
        {
            return Pessoas;
        }

        public static void DeletarPessoa(Guid identificador)
        {
            for (int i = 0; i < Pessoas.Count; i++)
            {
                if (identificador == Pessoas[i].Identificador)
                {
                    Transacao.Transacoes.RemoveAll(t => t.PessoaIdentificador == identificador);
                    Pessoas.RemoveAt(i);
                    return;
                }
            }
        }


        public void AdicionarTransacao(Transacao transacao)
        {
            if (transacao != null && transacao.PessoaIdentificador == Identificador)
            {
                Transacoes.Add(transacao);
            }
        }

        public List<Transacao> ListarTransacoes()
        {
            return Transacoes;
        }
    }
}
