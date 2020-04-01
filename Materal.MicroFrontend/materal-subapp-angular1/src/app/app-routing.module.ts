import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LocationStrategy, HashLocationStrategy } from '@angular/common';
import { TestComponent } from './test/test.component';
import {APP_BASE_HREF} from '@angular/common';
import { EmptyRouteComponent } from './empty-route/empty-route.component';


const routes: Routes = [
  { path: 'Test/:id', component: TestComponent },
  { path: 'Test', component: TestComponent },
  { path: '**', component: EmptyRouteComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [{ provide: APP_BASE_HREF, useValue: '/' }, { provide: LocationStrategy, useClass: HashLocationStrategy }]
})
export class AppRoutingModule { }
