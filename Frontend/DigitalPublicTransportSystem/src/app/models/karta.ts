export class Karta{
    Korisnik : string;
    TipKarte : string;
    TipPopusta : string;
    Cena : number;

    constructor(korisnik?: string, tipKarte?: string, tipPopusta?: string, cena?: number)
    {
        this.Korisnik = korisnik;
        this.TipKarte = tipKarte;
        this.TipPopusta = tipPopusta;
        this.Cena = cena;
    }
}