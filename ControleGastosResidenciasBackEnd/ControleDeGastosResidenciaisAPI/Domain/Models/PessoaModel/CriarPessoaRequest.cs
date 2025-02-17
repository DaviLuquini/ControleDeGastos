namespace ControleGastosResidenciaisAPI.Domain.Models.PessoaModel
{
    public class CriarPessoaRequest
    {
        //Request em DTO para abstrair e simplificar Request dos dados
        public string Nome { get; set; }
        public int Idade { get; set; }
    }

}
