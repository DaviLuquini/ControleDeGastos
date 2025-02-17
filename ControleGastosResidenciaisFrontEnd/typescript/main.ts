import { GerenciadorFinanceiro } from './gerenciadorFinanceiro';
import { Pessoa } from './pessoa';
import { Transacao } from './transacao';

// Event Listeners
const formularioPessoa = document.getElementById('formularioPessoa');
if(formularioPessoa) { 
    formularioPessoa.addEventListener('submit', (e) => {
        e.preventDefault();
        const nome = (document.getElementById('nome') as HTMLInputElement).value;
        const idadeString  = (document.getElementById('idade') as HTMLInputElement).value;
        const idade = parseInt(idadeString);
        Pessoa.adicionarPessoa(nome, idade);
        (e.target as HTMLFormElement).reset();
    });
} else {
    console.log('Formulario pessoa não encontrado');
}

const formularioTransacao = document.getElementById('formularioTransacao');
if(formularioTransacao){
    formularioTransacao.addEventListener('submit', (e) => {
        e.preventDefault();
        const descricao = (document.getElementById('descricao') as HTMLInputElement).value;
        const valorString = (document.getElementById('valor') as HTMLInputElement).value;
        const valor = parseFloat(valorString);
        const tipo = (document.getElementById('tipo') as HTMLInputElement).value;
        const pessoaIdentificador = (document.getElementById('pessoaId') as HTMLInputElement).value;

        Transacao.adicionarTransacao(descricao, valor, tipo as 'Receita' | 'Despesa', pessoaIdentificador);
        (e.target as HTMLFormElement).reset();
    });
} else {
    console.log('Formulario pessoa não encontrado');
}


// Inicialização
Pessoa.atualizarListaPessoas();
Transacao.atualizarListaTransacoes();
GerenciadorFinanceiro.atualizarResumo();