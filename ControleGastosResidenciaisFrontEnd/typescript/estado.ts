import { Transacao } from './transacao';
import { Pessoa } from './pessoa';
//Lista globais de pessoas e transações
export class Estado {
    static pessoas: Pessoa[] = [];
    static transacoes: Transacao[] = [];
}
