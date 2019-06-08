import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RedVoznjeComponent } from './red-voznje/forma/red-voznje.component';
import { CenovnikPrikazComponent } from './cenovnik/cenovnik-prikaz/cenovnik-prikaz.component';
import { KupovinaKarteComponent } from './kupovina-karte/kupovina-karte.component';
import { LoginComponent } from './login/login.component';
import { RegistracijaComponent } from './registracija/registracija.component';
import { DashboardComponent } from './admin/dashboard/dashboard.component';
import { NoviCenovnikComponent } from './admin/novi-cenovnik/novi-cenovnik.component';

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
    path: 'admin/dashboard',
    component: DashboardComponent
  },
  {
    path: 'noviCenovnik',
    component: NoviCenovnikComponent
  }
 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
