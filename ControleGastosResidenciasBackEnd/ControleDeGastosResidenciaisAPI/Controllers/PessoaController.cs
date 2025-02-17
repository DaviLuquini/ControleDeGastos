using ControleGastosResidenciaisAPI.Application;
using ControleGastosResidenciaisAPI.Domain.Models.PessoaModel;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeGastosResidenciaisAPI.Controllers
{
    [ApiController]
    [Route("pessoa")]
    //Controller para o Backend se conectar com o FrontEnd via API do ASP.NET
    public class PessoaController(PessoaAppService pessoaAppService) : Controller
    {
        private readonly PessoaAppService pessoaAppService = pessoaAppService;

        [HttpPost("adicionar")]
        public IActionResult AdicionarPessoa([FromBody] CriarPessoaRequest request)
        {
            var pessoaExistente = pessoaAppService.BuscarPessoaNome(request.Nome);

            if (pessoaExistente != null)
            {
                return Unauthorized(new {
                    code = "NOME_JA_UTILIZADO",
                    message = "Nome já utilizado por outra pessoa."
                });
            }

            var pessoa = pessoaAppService.CriarPessoa(request.Nome, request.Idade);

            return Ok(new { pessoa });
        }

        [HttpPost("deletar")]
        public IActionResult DeletarPessoa([FromBody] Guid identificador)
        {
            var pessoa = pessoaAppService.BuscarPessoaID(identificador);

            if (pessoa == null)
            {
                return NotFound(new {
                    code = "PESSOA_NAO_ENCONTRADA",
                    message = "Pessoa não encontrada."
                });
            }

            pessoaAppService.DeletarPessoa(identificador);

            return Ok(new { message = "Pessoa deletada com sucesso." });
        }

        [HttpGet("buscar-id")]
        public IActionResult BuscarPessoaID([FromQuery] Guid identificador)
        {
            var pessoa = pessoaAppService.BuscarPessoaID(identificador);

            if (pessoa == null)
            {
                return NotFound(new {
                    code = "PESSOA_NAO_ENCONTRADA",
                    message = "Pessoa não encontrada."
                });
            }

            return Ok(new { pessoa });
        }

        [HttpGet("buscar-nome")]
        public IActionResult BuscarPessoaNome([FromQuery] string nome)
        {
            var pessoa = pessoaAppService.BuscarPessoaNome(nome);

            if (pessoa == null)
            {
                return NotFound();
            }

            return Ok(new { pessoa });
        }

        [HttpGet("listar")]
        public IActionResult ListarPessoas()
        {
            var pessoas = pessoaAppService.ListarPessoas();

            return Ok(new { pessoas });
        }

    }
}
