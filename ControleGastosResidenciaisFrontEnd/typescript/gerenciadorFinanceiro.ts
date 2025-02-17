import { Transacao } from './transacao';
import { Pessoa } from './pessoa';
import { Estado } from './estado';

export class GerenciadorFinanceiro {
    static calcularTotaisPessoa(pessoaIdentificador: string) {
        const transacoesPessoa = Estado.transacoes.filter((t: Transacao) => t.pessoaIdentificador === pessoaIdentificador);
        const receitas = transacoesPessoa
            .filter((t: Transacao) => t.tipo === 'Receita')
            .reduce((soma: number, t: Transacao) => soma + t.valor, 0);
        const despesas = transacoesPessoa
            .filter((t: Transacao) => t.tipo === 'Despesa')
            .reduce((soma: number, t: Transacao) => soma + t.valor, 0);
        
        return {
            receitas,
            despesas,
            saldo: receitas - despesas
        };
    }

    static atualizarResumo() {
        const corpoResumo = document.getElementById('resumoFinanceiro') as HTMLElement;
        const resumoTotal = document.getElementById('resumoTotal') as HTMLElement;
        
        let totalReceitas = 0;
        let totalDespesas = 0;

        // Atualiza o resumo por pessoa
        corpoResumo.innerHTML = Estado.pessoas.map((pessoa: Pessoa) => {
            const totais = this.calcularTotaisPessoa(pessoa.identificador);
            totalReceitas += totais.receitas;
            totalDespesas += totais.despesas;
            return `
                <tr>
                    <td>${pessoa.nome}</td>
                    <td>${this.formatarMoeda(totais.receitas)}</td>
                    <td>${this.formatarMoeda(totais.despesas)}</td>
                    <td>${this.formatarMoeda(totais.saldo)}</td>
                </tr>
            `;
        }).join('');

        // Atualiza o total geral
        const saldoTotal = totalReceitas - totalDespesas;
        resumoTotal.innerHTML = `
            <tr>
                <td><strong>TOTAL GERAL</strong></td>
                <td>${this.formatarMoeda(totalReceitas)}</td>
                <td>${this.formatarMoeda(totalDespesas)}</td>
                <td>${this.formatarMoeda(saldoTotal)}</td>
            </tr>
        `;
    }

    // Formatar valor para BRL
    static formatarMoeda(valor: number) {
        return new Intl.NumberFormat('pt-BR', {
            style: 'currency',
            currency: 'BRL'
        }).format(valor);
    };
}