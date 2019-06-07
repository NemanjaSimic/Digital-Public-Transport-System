import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule }    from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RedVoznjeComponent } from './red-voznje/forma/red-voznje.component';
import { CenovnikPrikazComponent } from './cenovnik/cenovnik-prikaz/cenovnik-prikaz.component';
import { KupovinaKarteComponent } from './kupovina-karte/kupovina-karte.component';
import { NoviCenovnikComponent } from './cenovnik/novi-cenovnik/novi-cenovnik.component';
import { LoginComponent } from './login/login.component';
import { HttpModule } from '@angular/http';
import { NavbarComponent } from './navbar/navbar.component';
//import { RegistracijaComponent } from './registracija/registracija.component';


@NgModule({
  declarations: [
    AppComponent,
    RedVoznjeComponent,
    CenovnikPrikazComponent,
    KupovinaKarteComponent,
    NoviCenovnikComponent,
    LoginComponent,
    NavbarComponent,
   // RegistracijaComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    HttpModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
