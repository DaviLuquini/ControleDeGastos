using ControleGastosResidenciaisAPI.Domain.Models.PessoaModel;

namespace ControleDeGastosResidenciaisAPI.Application
{
    public interface IPessoaAppService
    {
        Pessoa CriarPessoa(string nome, int idade);
        Pessoa BuscarPessoaID(Guid identificador);
        Pessoa BuscarPessoaNome(string nome);
        void DeletarPessoa(Guid identificador);
        List<Pessoa> ListarPessoas();
    }
}
