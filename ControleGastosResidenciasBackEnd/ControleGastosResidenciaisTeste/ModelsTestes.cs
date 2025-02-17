using ControleGastosResidenciaisAPI.Domain.Models.PessoaModel;
using ControleGastosResidenciaisAPI.Domain.Models.TransacaoModel;

namespace ControleGastosResidenciaisTeste
{
    //Classe de testes para acompanhar processo de construção dos Models
    public class ModelsTestes
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ListarPessoa()
        {
            Pessoa pessoa1 = new("Davi", 20);
            Pessoa pessoa2 = new("Laura", 19);

            var result1 = Pessoa.BuscarPessoaID(pessoa1.Identificador);
            var result2 = Pessoa.BuscarPessoaID(pessoa2.Identificador);

            Assert.Multiple(() => {
                Assert.That(result1, Is.EqualTo(pessoa1));
                Assert.That(result2, Is.EqualTo(pessoa2));
            });
        }

        [Test]
        public void ListarPessoas()
        {
            Pessoa pessoa1 = new("Davi", 20);
            Pessoa pessoa2 = new("Laura", 19);

            var result = Pessoa.ListarPessoas();

            Assert.Multiple(() => {
                Assert.That(result[0], Is.EqualTo(pessoa1));
                Assert.That(result[1], Is.EqualTo(pessoa2));
            });
        }

        [Test]
        public void DeletarPessoa()
        {
            Pessoa pessoa1 = new("Davi", 20);
            Pessoa pessoa2 = new("Laura", 19);

            Pessoa.DeletarPessoa(pessoa2.Identificador);


            var result = Pessoa.ListarPessoas();

            Assert.Multiple(() =>
            {
                Assert.That(result, Does.Not.Contain(pessoa2)); 
                Assert.That(result, Has.Count.EqualTo(1)); 
                Assert.That(result[0], Is.EqualTo(pessoa1)); 
            });
        }

        [Test]
        public void DeletarPessoa_DeveRemover_TransacoesDela()
        {
            Pessoa pessoa1 = new("Davi", 20);
            Pessoa pessoa2 = new("Laura", 19);

            Transacao transacao1 = new(pessoa1.Identificador, "Salário do mês", 2000, TipoTransacao.Receita);
            Transacao transacao2 = new(pessoa1.Identificador, "Venda de carro", 50000, TipoTransacao.Receita);
            Transacao transacao3 = new(pessoa2.Identificador, "Mercado do mês", 1000, TipoTransacao.Despesa);

            Pessoa.DeletarPessoa(pessoa2.Identificador);

            var result = Transacao.ListarTransacoes();

            Assert.Multiple(() => {
                Assert.That(result, Does.Not.Contain(transacao3));
                Assert.That(result, Has.Count.EqualTo(2));
                Assert.That(result[0], Is.EqualTo(transacao1));
            });
        }

        [Test]
        public void Transacao_DeveFalhar_Se_PessoaForMenorDeIdade_E_TipoForReceita()
        {
            Pessoa pessoa1 = new("Pedro", 15);

            Transacao transacao1 = new(pessoa1.Identificador, "Videogame", 60, TipoTransacao.Despesa);

            Assert.Throws<InvalidOperationException>(() =>
                new Transacao(pessoa1.Identificador, "Salário do mês", 2000, TipoTransacao.Receita));
        }

        [Test]
        public void Transacao_DeveFalhar_Se_ValorNãoForPositivo()
        {
            Pessoa pessoa1 = new("Pedro", 15);

            Transacao transacao1 = new(pessoa1.Identificador, "Videogame", 60, TipoTransacao.Despesa);

            Assert.Throws<InvalidOperationException>(() =>
                new Transacao(pessoa1.Identificador, "Despesa do mês", -2000, TipoTransacao.Despesa));
        }

        [Test]
        public void ListarTransacao_Pessoais()
        {
            Pessoa pessoa1 = new("Davi", 20);
            Pessoa pessoa2 = new("Laura", 19);

            Transacao transacao1 = new(pessoa1.Identificador, "Salário do mês", 2000, TipoTransacao.Receita);
            Transacao transacao2 = new(pessoa1.Identificador, "Venda de carro", 50000, TipoTransacao.Receita);
            Transacao transacao3 = new(pessoa2.Identificador, "Mercado do mês", 1000, TipoTransacao.Despesa);

            pessoa1.AdicionarTransacao(transacao1);
            pessoa1.AdicionarTransacao(transacao2);
            pessoa2.AdicionarTransacao(transacao3);

            var result1 = pessoa1.ListarTransacoes();
            var result2 = pessoa2.ListarTransacoes();

            Assert.Multiple(() => {
                Assert.That(result1[0].Identificador, Is.EqualTo(transacao1.Identificador));
                Assert.That(result1[1].Identificador, Is.EqualTo(transacao2.Identificador));
                Assert.That(result2[0].Identificador, Is.EqualTo(transacao3.Identificador));
            });
        }

        [Test]
        public void ListarTransacao_Gerais()
        {
            Pessoa pessoa1 = new("Davi", 20);
            Pessoa pessoa2 = new("Laura", 19);

            Transacao transacao1 = new(pessoa1.Identificador, "Salário do mês", 2000, TipoTransacao.Receita);
            Transacao transacao2 = new(pessoa1.Identificador, "Venda de carro", 50000, TipoTransacao.Receita);
            Transacao transacao3 = new(pessoa2.Identificador, "Mercado do mês", 1000, TipoTransacao.Despesa);

            pessoa1.AdicionarTransacao(transacao1);
            pessoa1.AdicionarTransacao(transacao2);
            pessoa2.AdicionarTransacao(transacao3);

            var result = Transacao.ListarTransacoes();

            Assert.Multiple(() => {
                Assert.That(result[0].Identificador, Is.EqualTo(transacao1.Identificador));
                Assert.That(result[1].Identificador, Is.EqualTo(transacao2.Identificador));
                Assert.That(result[2].Identificador, Is.EqualTo(transacao3.Identificador));
            });
        }
    }
}