import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UserListComponent } from './user-list/user-list.component';
import { ProjectListComponent } from './project-list/project-list.component';
import { ConfigurationItemListComponent } from './configuration-item-list/configuration-item-list.component';
import { NamespaceListComponent } from './namespace-list/namespace-list.component';

const routes: Routes = [
  { path: 'UserList', component: UserListComponent },
  { path: 'ProjectList', component: ProjectListComponent },
  { path: 'NamespaceList', component: NamespaceListComponent },
  { path: 'NamespaceList/:id', component: NamespaceListComponent },
  { path: 'ConfigurationItemList', component: ConfigurationItemListComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PagesRoutingModule { }
