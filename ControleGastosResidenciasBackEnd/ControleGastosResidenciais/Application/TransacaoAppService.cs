using ControleGastosResidenciais.Domain.Models.TransacaoModel;

namespace ControleGastosResidenciais.Application
{
    //Camada de abstração para conectar Models(Domain) para o Controller(Presentation)
    public class TransacaoAppService(TransacaoFactory transacaoFactory)
    {
        private readonly TransacaoFactory transacaoFactory = transacaoFactory;

        public Transacao CriarTransacao(Guid pessoaIdentificador, string descricao, decimal valor, TipoTransacao tipo)
        {
            var novaTransacao = transacaoFactory.CriarTransacao(pessoaIdentificador, descricao, valor, tipo);

            return novaTransacao;
        }

        public List<Transacao> ListarTransacoes()
        {
            return Transacao.ListarTransacoes();
        }
    }
}
