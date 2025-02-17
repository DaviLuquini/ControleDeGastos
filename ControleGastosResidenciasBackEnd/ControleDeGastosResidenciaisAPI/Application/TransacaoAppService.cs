using ControleDeGastosResidenciaisAPI.Application;
using ControleGastosResidenciaisAPI.Domain.Models.TransacaoModel;

namespace ControleGastosResidenciaisAPI.Application
{
    //Camada de abstração para conectar Models(Domain) para o Controller(Presentation)
    public class TransacaoAppService(TransacaoFactory transacaoFactory) : ITransacaoAppService
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
