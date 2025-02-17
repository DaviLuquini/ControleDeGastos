namespace ControleGastosResidenciaisAPI.Domain.Models.PessoaModel
{
    public class PessoaFactory
    {
        //Factory para simplificar criação de objeto
        public Pessoa CriarPessoa(string nome, int idade)
        {
            return new Pessoa(nome, idade);
        }
    }
}
