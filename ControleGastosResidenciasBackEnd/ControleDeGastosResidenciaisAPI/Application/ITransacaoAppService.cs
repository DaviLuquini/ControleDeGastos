using ControleGastosResidenciaisAPI.Domain.Models.TransacaoModel;

namespace ControleDeGastosResidenciaisAPI.Application
{
    public interface ITransacaoAppService
    {
        Transacao CriarTransacao(Guid pessoaIdentificador, string descricao, decimal valor, TipoTransacao tipo);
        List<Transacao> ListarTransacoes();
    }
}
