import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { IndexComponent } from './index/index.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { LoginGuard } from './login.guard';


const routes: Routes = [
  { path: 'Login', component: LoginComponent },
  {
    path: 'Index', component: IndexComponent,
    loadChildren: () => import('./pages/pages.module').then(mod => mod.PagesModule),
    canActivate: [LoginGuard]
  },
  { path: '', pathMatch: 'full', redirectTo: '/Index/AppList' },
  { path: '**', component: PageNotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
