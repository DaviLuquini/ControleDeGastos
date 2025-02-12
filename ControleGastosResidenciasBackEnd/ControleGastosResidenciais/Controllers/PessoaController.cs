using ControleGastosResidenciais.Application;
using ControleGastosResidenciais.Domain.Models;
using ControleGastosResidenciais.Domain.Models.PessoaModel;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastosResidenciais.Controllers
{
    //Controller para o Backend se conectar com o FrontEnd via API do ASP.NET
    public class PessoaController(PessoaAppService pessoaAppService) : Controller
    {
        private readonly PessoaAppService pessoaAppService = pessoaAppService;

        [HttpPost]
        public IActionResult AdicionarPessoa([FromBody] CriarPessoaRequest request)
        {
            var pessoa = pessoaAppService.CriarPessoa(request.Nome, request.Idade);

            return Ok(new { pessoa });
        }

        [HttpPost]
        public IActionResult ExcluirPessoa([FromBody] Guid identificador)
        {
            pessoaAppService.ExcluirPessoa(identificador);

            return Ok();
        }

        [HttpGet]
        public IActionResult BuscarPessoaID([FromBody] Guid identificador)
        {
            var pessoa = pessoaAppService.BuscarPessoaID(identificador);

            return Ok(new { pessoa });
        }

        [HttpGet]
        public IActionResult BuscarPessoaNome([FromBody] string nome)
        {
            var pessoa = pessoaAppService.BuscarPessoaNome(nome);

            return Ok(new { pessoa });
        }

        [HttpGet]
        public IActionResult ListarPessoas()
        {
            var pessoas = pessoaAppService.ListarPessoas();

            return Ok(new { pessoas });
        }

    }
}
