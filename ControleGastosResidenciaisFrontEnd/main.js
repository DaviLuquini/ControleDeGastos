// Estado da aplicação
let state = {
    people: [],
    transactions: [],
    nextPersonId: 1,
    nextTransactionId: 1
};

// Funções auxiliares
const formatCurrency = (value) => {
    return new Intl.NumberFormat('pt-BR', {
        style: 'currency',
        currency: 'BRL'
    }).format(value);
};

// Gerenciamento de Pessoas
class PersonManager {
    static addPerson(name, age) {
        const person = {
            id: state.nextPersonId++,
            name,
            age: parseInt(age)
        };
        state.people.push(person);
        this.updatePeopleList();
        this.updatePersonSelect();
        FinancialManager.updateSummary();
        return person;
    }

    static deletePerson(id) {
        // Remove todas as transações da pessoa
        state.transactions = state.transactions.filter(t => t.personId !== id);
        // Remove a pessoa
        state.people = state.people.filter(p => p.id !== id);
        this.updatePeopleList();
        this.updatePersonSelect();
        FinancialManager.updateSummary();
    }

    static updatePeopleList() {
        const peopleList = document.getElementById('peopleList');
        peopleList.innerHTML = state.people.map(person => `
            <tr>
                <td>${person.id}</td>
                <td>${person.name}</td>
                <td>${person.age}</td>
                <td>
                    <span class="btn-delete" onclick="PersonManager.deletePerson(${person.id})">
                        🗑️ Excluir
                    </span>
                </td>
            </tr>
        `).join('');
    }

    static updatePersonSelect() {
        const personSelect = document.getElementById('personId');
        personSelect.innerHTML = state.people.map(person => 
            `<option value="${person.id}">${person.name}</option>`
        ).join('');
    }
}

// Gerenciamento de Transações
class TransactionManager {
    static addTransaction(description, amount, type, personId) {
        const person = state.people.find(p => p.id === parseInt(personId));
        
        // Verifica se é menor de idade e está tentando adicionar receita
        if (person.age < 18 && type === 'receita') {
            alert('Menores de idade só podem registrar despesas!');
            return null;
        }

        const transaction = {
            id: state.nextTransactionId++,
            description,
            amount: parseFloat(amount),
            type,
            personId: parseInt(personId)
        };

        state.transactions.push(transaction);
        FinancialManager.updateSummary();
        return transaction;
    }
}

// Gerenciamento Financeiro
class FinancialManager {
    static calculatePersonTotals(personId) {
        const personTransactions = state.transactions.filter(t => t.personId === personId);
        const receitas = personTransactions
            .filter(t => t.type === 'receita')
            .reduce((sum, t) => sum + t.amount, 0);
        const despesas = personTransactions
            .filter(t => t.type === 'despesa')
            .reduce((sum, t) => sum + t.amount, 0);
        
        return {
            receitas,
            despesas,
            saldo: receitas - despesas
        };
    }

    static updateSummary() {
        const summaryBody = document.getElementById('financialSummary');
        const totalSummary = document.getElementById('totalSummary');
        
        let totalReceitas = 0;
        let totalDespesas = 0;

        // Atualiza o resumo por pessoa
        summaryBody.innerHTML = state.people.map(person => {
            const totals = this.calculatePersonTotals(person.id);
            totalReceitas += totals.receitas;
            totalDespesas += totals.despesas;

            return `
                <tr>
                    <td>${person.name}</td>
                    <td>${formatCurrency(totals.receitas)}</td>
                    <td>${formatCurrency(totals.despesas)}</td>
                    <td>${formatCurrency(totals.saldo)}</td>
                </tr>
            `;
        }).join('');

        // Atualiza o total geral
        const saldoTotal = totalReceitas - totalDespesas;
        totalSummary.innerHTML = `
            <tr>
                <td><strong>TOTAL GERAL</strong></td>
                <td>${formatCurrency(totalReceitas)}</td>
                <td>${formatCurrency(totalDespesas)}</td>
                <td>${formatCurrency(saldoTotal)}</td>
            </tr>
        `;
    }
}

// Event Listeners
document.getElementById('personForm').addEventListener('submit', (e) => {
    e.preventDefault();
    const name = document.getElementById('name').value;
    const age = document.getElementById('age').value;
    PersonManager.addPerson(name, age);
    e.target.reset();
});

document.getElementById('transactionForm').addEventListener('submit', (e) => {
    e.preventDefault();
    const description = document.getElementById('description').value;
    const amount = document.getElementById('amount').value;
    const type = document.getElementById('type').value;
    const personId = document.getElementById('personId').value;
    
    const transaction = TransactionManager.addTransaction(description, amount, type, personId);
    if (transaction) {
        e.target.reset();
    }
});

// Inicialização
PersonManager.updatePeopleList();
PersonManager.updatePersonSelect();
FinancialManager.updateSummary();