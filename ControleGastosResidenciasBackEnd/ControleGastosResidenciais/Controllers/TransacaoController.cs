using ControleGastosResidenciais.Application;
using ControleGastosResidenciais.Domain.Models.PessoaModel;
using ControleGastosResidenciais.Domain.Models.TransacaoModel;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastosResidenciais.Controllers
{
    //Controller para o Backend se conectar com o FrontEnd via API do ASP.NET
    public class TransacaoController(TransacaoAppService transacaoAppService) : Controller
    {
        private readonly TransacaoAppService transacaoAppService = transacaoAppService;

        [HttpPost]
        public IActionResult AdicionarTransacao([FromBody] CriarTransacaoRequest request)
        {
            var transacao = transacaoAppService.CriarTransacao(request.PessoaIdentificador, request.Descricao, request.Valor,  request.Tipo);

            return Ok(new { transacao });
        }

        [HttpGet]
        public IActionResult ListarTransacoes()
        {
            var transacoes = transacaoAppService.ListarTransacoes();

            return Ok(new { transacoes });
        }
    }
}
