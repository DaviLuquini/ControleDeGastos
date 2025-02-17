namespace ControleGastosResidenciaisAPI.Domain.Models.TransacaoModel
{
    public class TransacaoFactory
    {
        //Factory para simplificar criação de objeto
        public Transacao CriarTransacao(Guid pessoaIdentificador, string descricao, decimal valor, TipoTransacao tipo)
        {
            return new Transacao(pessoaIdentificador, descricao, valor, tipo);
        }
    }
}
