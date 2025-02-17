using ControleDeGastosResidenciaisAPI.Application;
using ControleGastosResidenciaisAPI.Domain.Models.PessoaModel;

namespace ControleGastosResidenciaisAPI.Application
{
    public class PessoaAppService(PessoaFactory pessoaFactory) : IPessoaAppService
    {
        //Camada de abstração para conectar Models(Domain) para o Controller(Presentation)
        private readonly PessoaFactory pessoaFactory = pessoaFactory;
        public Pessoa CriarPessoa(string nome, int idade)
        {
            var novaPessoa = pessoaFactory.CriarPessoa(nome, idade);

            return novaPessoa;
        }

        public Pessoa BuscarPessoaID(Guid identificador)
        {
            return Pessoa.BuscarPessoaID(identificador);
        }

        public Pessoa BuscarPessoaNome(string nome)
        {
            return Pessoa.BuscarPessoaNome(nome);
        }


        public void DeletarPessoa(Guid identificador)
        {
            Pessoa.DeletarPessoa(identificador);
        }
        public List<Pessoa> ListarPessoas()
        {
            return Pessoa.ListarPessoas();
        }
    }
}
