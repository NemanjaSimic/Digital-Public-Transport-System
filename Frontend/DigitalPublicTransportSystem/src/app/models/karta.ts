export class Karta{
    Korisnik : string;
    TipKarte : string;
    TipPopusta : string;
    Cena : number;
    Datum : string;
    Id: string;

    constructor(korisnik?: string, tipKarte?: string, tipPopusta?: string, cena?: number, datum?: string, id?: string)
    {
        this.Korisnik = korisnik;
        this.TipKarte = tipKarte;
        this.TipPopusta = tipPopusta;
        this.Cena = cena;
        this.Datum = datum;
        this.Id = id;
    }
}