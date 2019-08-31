import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { KartaService } from '../services/karta.service';
import { Karta } from '../models/karta';
import { IPayPalConfig, ICreateOrderRequest} from 'ngx-paypal';

@Component({
  selector: 'app-kupovina-karte',
  templateUrl: './kupovina-karte.component.html',
  styleUrls: ['./kupovina-karte.component.css']
})
export class KupovinaKarteComponent implements OnInit {

  public payPalConfig ? : IPayPalConfig;

  constructor(private fb: FormBuilder,private route: ActivatedRoute, private kartaService : KartaService,
    private router: Router) { }

  emailForm = this.fb.group({
    email : ["", Validators.email]
  });

  tipKarte: string;
  tipPopusta: string;
  cena: number;
  role:any;

  get f() { return this.emailForm.controls; }

  ngOnInit() {
    this.getInfo();
    this.initConfig();
  }

  getInfo(): void{
    this.tipKarte = this.route.snapshot.paramMap.get('tip');
    this.tipPopusta = this.route.snapshot.paramMap.get('popust');
    this.cena = +this.route.snapshot.paramMap.get('cena');
    this.role = localStorage.role;
  }

  kupiKartu(): void{
    this.kartaService.kupiKartuNeregistrovani(this.emailForm.get('email').value).subscribe(
      response => {
        alert("Uspesno ste kupili vremensku kartu u trajanju od 60 min. Poslali smo Vam sifru karte na Vas email.");
        this.router.navigate(['/']);
      },
      error => {
        alert("Desila se greska prilikom kupovine karte. Pokusajte ponovo.");
      }
    )
  }

  potvrdiKupovinu():void{
    let karta = new Karta(localStorage.getItem('userId'), this.tipKarte, this.tipPopusta, this.cena);
    this.kartaService.kupiKartuRegistrovani(karta).subscribe(
      response => {
        alert("Uspesno ste kupili kartu tipa ->"  + this.tipKarte);
        this.router.navigate(['/']);
      },
      error => {
        alert("Vas dokument nije validiran ili Vam nije dozvoljeno da kupite izabran tip karte. Pokusajte ponovo.");
      }
    )
  }

  initConfig(): void {
    this.payPalConfig = {
        currency: 'EUR',
        clientId: 'AQa4wylDnfRrA9zcrWSWNLrNZLIpXqj52jVoFKpAd3eeJWokew-G4VD4D2rl_L9-xyElUaWMcMOmXdxA',
        createOrderOnClient: (data) => < ICreateOrderRequest > {
            intent: 'CAPTURE',
            purchase_units: [{
                amount: {
                    currency_code: 'EUR',
                    value: `${this.cena}`,
                    breakdown: {
                        item_total: {
                            currency_code: 'EUR',
                            value: `${this.cena}`
                        }
                    }
                },
                items: [{
                    name: `${this.tipPopusta} ${this.tipKarte}`,
                    quantity: '1',
                    category: 'DIGITAL_GOODS',
                    unit_amount: {
                        currency_code: 'EUR',
                        value: `${this.cena}`,
                    },
                }]
            }]
        },
        advanced: {
            commit: 'true'
        },
        style: {
            label: 'paypal',
            layout: 'vertical'
        },
        onApprove: (data, actions) => {
            console.log('onApprove - transaction was approved, but not authorized', data, actions);
            actions.order.get().then(details => {
                console.log('onApprove - you can get full order details inside onApprove: ', details);
            });

        },
        onClientAuthorization: (data) => {
            console.log('onClientAuthorization - you should probably inform your server about completed transaction at this point', data);
            if(this.role != null){
              let karta = new Karta(localStorage.getItem('userId'), this.tipKarte, this.tipPopusta, this.cena,'', data.id);
              this.kartaService.kupiKartuRegistrovani(karta).subscribe(
                response => {
                  alert("Uspesno ste kupili kartu tipa ->"  + this.tipKarte);
                  this.router.navigate(['/']);
                },
                error => {
                  alert("Vas dokument nije validiran ili Vam nije dozvoljeno da kupite izabran tip karte. Pokusajte ponovo.");
                }
              )
            }else {
              this.kartaService.kupiKartuNeregistrovani(this.emailForm.get('email').value+'-'+data.id).subscribe(
                response => {
                  alert("Uspesno ste kupili vremensku kartu u trajanju od 60 min. Poslali smo Vam sifru karte na Vas email.");
                  this.router.navigate(['/']);
                },
                error => {
                  alert("Desila se greska prilikom kupovine karte. Pokusajte ponovo.");
                }
              )
            }
        },
        onCancel: (data, actions) => {
            console.log('OnCancel', data, actions);
        },
        onError: err => {
            console.log('OnError', err);
        },
        onClick: (data, actions) => {
            console.log('onClick', data, actions);
        },
    };
}

}
