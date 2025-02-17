using ControleGastosResidenciaisAPI.Application;
using ControleGastosResidenciaisAPI.Domain.Models.PessoaModel;
using ControleGastosResidenciaisAPI.Domain.Models.TransacaoModel;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeGastosResidenciaisAPI.Controllers
{
    [ApiController]
    [Route("transacao")]
    //Controller para o Backend se conectar com o FrontEnd via API do ASP.NET
    public class TransacaoController(TransacaoAppService transacaoAppService) : Controller
    {
        private readonly TransacaoAppService transacaoAppService = transacaoAppService;

        [HttpPost("adicionar")]
        public IActionResult AdicionarTransacao([FromBody] CriarTransacaoRequest request)
        {
            var transacao = transacaoAppService.CriarTransacao(request.PessoaIdentificador, request.Descricao, request.Valor,  request.Tipo);

            return Ok(new { transacao });
        }

        [HttpGet("listar")]
        public IActionResult ListarTransacoes()
        {
            var transacoes = transacaoAppService.ListarTransacoes();

            return Ok(new { transacoes });
        }
    }
}
