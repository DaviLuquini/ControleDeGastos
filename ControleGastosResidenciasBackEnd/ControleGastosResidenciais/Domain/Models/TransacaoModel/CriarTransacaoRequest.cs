namespace ControleGastosResidenciais.Domain.Models.TransacaoModel
{
    public class CriarTransacaoRequest
    {
        //Request em DTO para abstrair e simplificar Request dos dados
        public Guid PessoaIdentificador { get; set; }
        public string Descricao { get; set; }
        public decimal Valor {  get; set; }
        public TipoTransacao Tipo { get; set; }
    }
}
