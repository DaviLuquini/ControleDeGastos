import { GerenciadorFinanceiro } from './gerenciadorFinanceiro';
import { Estado } from './estado';

export class Transacao {
    identificador: string;
    descricao: string;
    valor: number;
    tipo: 'Receita' | 'Despesa';
    pessoaIdentificador: string;

    constructor(identificador: string, descricao: string, valor: number, tipo: 'Receita' | 'Despesa', pessoaIdentificador: string) {
        this.identificador = identificador;
        this.descricao = descricao;
        this.valor = valor;
        this.tipo = tipo;
        this.pessoaIdentificador = pessoaIdentificador;
    }

    //Funções para manipular transação via API
    static async adicionarTransacao(descricao: string, valor: number, tipo: 'Receita' | 'Despesa', pessoaIdentificador: string) {
        let result;

        const pessoa = Estado.pessoas.find((p: { identificador: string; }) => p.identificador === pessoaIdentificador);
        
        if (!pessoa) {
            alert('Pessoa não encontrada!');
            return null;
        }

        if (pessoa.idade < 18 && tipo === 'Receita') {
            alert('Menores de 18 anos só podem registrar despesas.');
            return null;
        }

        try {
            const response = await fetch('https://localhost:7163/transacao/adicionar', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },

                body: JSON.stringify({ descricao, valor, tipo, pessoaIdentificador })
            });

            result = await response.json();

            if (response.ok) {      
                console.log("Transação adicionada com sucesso");

                Transacao.atualizarListaTransacoes();
                return result;
            } else {
                alert('Conexão com o servidor falhou. Tente novamente.');
            }

        } catch (error) {
            alert('Conexão com o servidor falhou. Tente novamente.');
            console.error(error);
        }
    }

    static async atualizarListaTransacoes() {
        try {
            const response = await fetch('https://localhost:7163/transacao/listar', {
                method: 'GET',
                headers: {
                    'Accept': '*/*',
                    'Content-Type': 'application/json'
                }
            });

            const result = await response.json();

            if (response.ok) {
                console.log("Resposta da API Transações:", result);

                const transacoes = result.transacoes.map((transacao: { identificador: string, descricao: string, valor: number, tipo: 'Receita' | 'Despesa', pessoaIdentificador: string }) =>
                    new Transacao(transacao.identificador, transacao.descricao, transacao.valor, transacao.tipo, transacao.pessoaIdentificador)
                );

                Estado.transacoes = transacoes;
                GerenciadorFinanceiro.atualizarResumo();
                return transacoes;
            } else {
                alert('Conexão com o servidor falhou. Tente novamente.');
            }

        } catch (error) {
            alert('Conexão com o servidor falhou. Tente novamente.');
            console.error(error);
        }
    }
}
