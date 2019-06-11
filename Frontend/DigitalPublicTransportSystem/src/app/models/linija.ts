import { Stanica } from './stanica';

export class Linija{
    ID : number;
    Ime: string;
    TipLinije: number;
    RedniBroj: number;
    Termini: [];
    Stanice: Stanica[];
}