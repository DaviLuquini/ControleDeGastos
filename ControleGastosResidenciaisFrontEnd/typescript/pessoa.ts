import { Transacao } from './transacao';
import { GerenciadorFinanceiro } from './gerenciadorFinanceiro';
import { Estado } from './estado';

export class Pessoa {
    identificador: string;
    nome: string;
    idade: number;
    transacoes: Transacao[];
    static pessoas: Pessoa[] = [];

    constructor(identificador: string, nome: string, idade: number, transacoes: Transacao[]) {
        this.identificador = identificador;
        this.nome = nome;
        this.idade = idade;
        this.transacoes = transacoes;
    }

    //Funções para manipular pessoa via API
    static async adicionarPessoa(nome: string, idade: number) {
        let result;
        if(idade < 0 || idade > 120) { 
            alert('Adicione uma idade real!');
            return null;
        }

        try {
            const response = await fetch('https://localhost:7163/pessoa/adicionar', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },

                body: JSON.stringify({ nome, idade })
            });

            result = await response.json();

            if (response.ok) {
                console.log("Pessoa adicionada com sucesso");
                Pessoa.atualizarListaPessoas();
                return result;
            }
            else if (result.code === 'NOME_JA_UTILIZADO') {
                alert(result.message);
            }

            else {
                alert('Conexão com o servidor falhou. Tente novamente.');
            }

        } catch (error) {
            alert('Conexão com o servidor falhou. Tente novamente.');
            console.error(error);
        }
    }

    static async deletarPessoa(identificador: string) {
        try {
            const response = await fetch('https://localhost:7163/pessoa/deletar', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },

                body: JSON.stringify(identificador)
            });

            const result = await response.json();

            if (response.ok) {
                console.log("Pessoa deletada com sucesso");
            }
            else if (result.code === 'PESSOA_NAO_ENCONTRADA') {
                alert(result.message);
            }

            else {
                alert('Conexão com o servidor falhou. Tente novamente.');
            }

        } catch (error) {
            alert('Conexão com o servidor falhou. Tente novamente.');
            console.error(error);
        }

        Pessoa.atualizarListaPessoas();
        GerenciadorFinanceiro.atualizarResumo();
    }

    static async atualizarListaPessoas() {
        try {
            const response = await fetch('https://localhost:7163/pessoa/listar', {
                method: 'GET',
                headers: {
                    'Accept': '*/*',
                    'Content-Type': 'application/json'
                }
            });

            const result = await response.json();

            if (response.ok) {
                console.log("Resposta da API Pessoas:", result);

                const pessoas = result.pessoas.map((pessoa: { nome: string, idade: number, identificador: string, transacoes: any[] }) =>
                    new Pessoa(pessoa.identificador, pessoa.nome, pessoa.idade, pessoa.transacoes)
                );

                const listaPessoas = document.getElementById('listaPessoas') as HTMLElement;
                listaPessoas.innerHTML = pessoas.map((pessoa: Pessoa) => `
                <tr>
                    <td>${pessoa.nome}</td>
                    <td>${pessoa.idade}</td>
                    <td>
                        <span class="btn-delete" onclick="Pessoa.deletarPessoa('${pessoa.identificador}')">
                            🗑️ Excluir
                        </span>
                    </td>
                </tr>
            `).join('');

                Pessoa.atualizarSelecaoPessoa(pessoas);
                Estado.pessoas = pessoas;
                GerenciadorFinanceiro.atualizarResumo();
                return pessoas;
            } else {
                alert('Conexão com o servidor falhou. Tente novamente.');
            }

        } catch (error) {
            alert('Conexão com o servidor falhou. Tente novamente.');
            console.error(error);
        }
    }

    static async atualizarSelecaoPessoa(pessoas: Pessoa[] = []) {
        const selecaoPessoa = document.getElementById('pessoaId') as HTMLElement;

        selecaoPessoa.innerHTML = pessoas.map((pessoa: Pessoa) =>
            `<option value="${pessoa.identificador}">${pessoa.nome}</option>`
        ).join('');
    }
}

(window as any).Pessoa = Pessoa;
