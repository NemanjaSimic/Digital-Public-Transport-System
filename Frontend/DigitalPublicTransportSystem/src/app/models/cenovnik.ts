export class Stavka{
    VrstaKarte: string;
    VrstaPopusta: string;
    Cena: number;

    constructor(vrstaKarte: string, vrstaPopusta: string, cena: number){
        this.VrstaPopusta = vrstaPopusta;
        this.VrstaKarte = vrstaKarte;
        this.Cena = cena;
    }
}
