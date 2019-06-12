import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS }    from '@angular/common/http';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { AgmCoreModule } from '@agm/core';
import {NgMultiSelectDropDownModule} from 'ng-multiselect-dropdown';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RedVoznjeComponent } from './red-voznje/forma/red-voznje.component';
import { CenovnikPrikazComponent } from './cenovnik/cenovnik-prikaz/cenovnik-prikaz.component';
import { KupovinaKarteComponent } from './kupovina-karte/kupovina-karte.component';
import { NoviCenovnikComponent } from './admin/novi-cenovnik/novi-cenovnik.component';
import { LoginComponent } from './login/login.component';
import { HttpModule } from '@angular/http';
import { NavbarComponent } from './navbar/navbar.component';
import { RegistracijaComponent } from './registracija/registracija.component';
import { ProfilComponent } from './profil/profil.component';
import { DashboardComponent } from './admin/dashboard/dashboard.component';
import { EditProfilComponent } from './edit-profil/edit-profil.component';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { NapraviLinijuComponent } from './admin/linija/napravi-liniju/napravi-liniju.component';
import { EditLinijaComponent } from './admin/linija/edit-linija/edit-linija.component';
import { NovaStanicaComponent } from './admin/stanica/nova-stanica/nova-stanica.component';
import { EditStanicaComponent } from './admin/stanica/edit-stanica/edit-stanica.component';
import { TokenInterceptor } from './interceptor/token.interceptor';
import { MrezaLinijaComponent } from './mreza-linija/mreza-linija.component';
import { ValidacijaDokumenataComponent } from './kontrolor/validacija-dokumenata/validacija-dokumenata.component';
import { DashboardKontrolorComponent } from './kontrolor/dashboard-kontrolor/dashboard-kontrolor.component';
import { DeaktivirajProfilComponent } from './deaktiviraj-profil/deaktiviraj-profil.component';


@NgModule({
  declarations: [
    AppComponent,
    RedVoznjeComponent,
    CenovnikPrikazComponent,
    KupovinaKarteComponent,
    NoviCenovnikComponent,
    LoginComponent,
    NavbarComponent,
    RegistracijaComponent,
    ProfilComponent,
    DashboardComponent,
    NapraviLinijuComponent,
    EditLinijaComponent,
    EditProfilComponent,
    ChangePasswordComponent,
    NapraviLinijuComponent,
    NovaStanicaComponent,
    EditStanicaComponent,
    MrezaLinijaComponent,
    ValidacijaDokumenataComponent,
    DashboardKontrolorComponent,
    DeaktivirajProfilComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    HttpModule,
    FormsModule,
    AgmCoreModule.forRoot({apiKey: 'AIzaSyDnihJyw_34z5S1KZXp90pfTGAqhFszNJk'}),
    NgMultiSelectDropDownModule.forRoot()
  ],
  providers: [{provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi:true}],
  bootstrap: [AppComponent]
})
export class AppModule { }
