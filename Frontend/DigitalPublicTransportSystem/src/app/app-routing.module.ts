import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RedVoznjeComponent } from './red-voznje/forma/red-voznje.component';
import { CenovnikPrikazComponent } from './cenovnik/cenovnik-prikaz/cenovnik-prikaz.component';
import { KupovinaKarteComponent } from './kupovina-karte/kupovina-karte.component';
import { LoginComponent } from './login/login.component';
import { RegistracijaComponent } from './registracija/registracija.component';
import { ProfilComponent } from './profil/profil.component';
import { DashboardComponent } from './admin/dashboard/dashboard.component';
import { NoviCenovnikComponent } from './admin/novi-cenovnik/novi-cenovnik.component';
import { EditProfilComponent } from './edit-profil/edit-profil.component';
import { ChangePassModel } from './models/changePassModel';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { NapraviLinijuComponent } from './admin/linija/napravi-liniju/napravi-liniju.component';
import { EditLinijaComponent } from './admin/linija/edit-linija/edit-linija.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: '/redVoznje',
    pathMatch: 'full'
  },
  {
    path: 'redVoznje',
    component: RedVoznjeComponent
  },
  {
    path: 'cenovnik',
    component: CenovnikPrikazComponent
  },
  {
    path: 'kupiKartu/:tip/:popust/:cena',
    component: KupovinaKarteComponent
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'registracija',
    component: RegistracijaComponent
  },
  {
    path: 'profil',
    component: ProfilComponent
   },
   {
    path: 'admin/dashboard',
    component: DashboardComponent
  },
  {
    path: 'admin/noviCenovnik',
    component: NoviCenovnikComponent
  },
  {
    path: 'editProfil',
    component: EditProfilComponent
  },
  {
    path: 'changePassword',
    component: ChangePasswordComponent
   },
  {
    path: 'admin/novaLinija',
    component: NapraviLinijuComponent
  },
  {
    path: 'admin/editLinija',
    component: EditLinijaComponent
  }
 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
