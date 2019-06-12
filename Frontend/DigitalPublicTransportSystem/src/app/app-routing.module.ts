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
import { NovaStanicaComponent } from './admin/stanica/nova-stanica/nova-stanica.component';
import { EditStanicaComponent } from './admin/stanica/edit-stanica/edit-stanica.component';
import { ValidacijaDokumenataComponent } from './kontrolor/validacija-dokumenata/validacija-dokumenata.component';
import { DashboardKontrolorComponent } from './kontrolor/dashboard-kontrolor/dashboard-kontrolor.component';
import { MrezaLinijaComponent } from './mreza-linija/mreza-linija.component';
import { DeaktivirajProfilComponent } from './deaktiviraj-profil/deaktiviraj-profil.component';
import { AdminGuard } from './guards/admin.guard';
import { KontrolorGuard } from './guards/kontrolor.guard';

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
    path: 'mreza',
    component: MrezaLinijaComponent
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
    component: DashboardComponent,
    canActivate: [AdminGuard]
  },
  {
    path: 'admin/noviCenovnik',
    component: NoviCenovnikComponent,
    canActivate: [AdminGuard]
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
    component: NapraviLinijuComponent,
    canActivate: [AdminGuard]
  },
  {
    path: 'admin/editLinija',
    component: EditLinijaComponent,
    canActivate: [AdminGuard]
  },
  {
    path: 'admin/napraviStanicu',
    component: NovaStanicaComponent,
    canActivate: [AdminGuard]
  },
  {
    path: 'admin/editStanica',
    component: EditStanicaComponent,
    canActivate: [AdminGuard]
  },
  {
    path: 'kontrolor/dashboard',
    component: DashboardKontrolorComponent,
    canActivate: [KontrolorGuard]
  },
   {
    path: 'deaktivirajProfil',
    component: DeaktivirajProfilComponent
   },
   {
    path: 'kontrolor/validacijaDokumenata',
    component: ValidacijaDokumenataComponent,
    canActivate: [KontrolorGuard]
  }
 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
