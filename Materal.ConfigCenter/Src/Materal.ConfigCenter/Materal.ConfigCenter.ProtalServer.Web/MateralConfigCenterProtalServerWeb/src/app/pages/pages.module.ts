import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NgZorroAntdModule } from 'ng-zorro-antd';
import { PagesRoutingModule } from './pages-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UserListComponent } from './user-list/user-list.component';
import { UserEditComponent } from './user-edit/user-edit.component';
import { ProjectListComponent } from './project-list/project-list.component';
import { NamespaceEditComponent } from './namespace-edit/namespace-edit.component';
import { ProjectEditComponent } from './project-edit/project-edit.component';
import { ConfigurationItemListComponent } from './configuration-item-list/configuration-item-list.component';
import { NamespaceListComponent } from './namespace-list/namespace-list.component';
import { ProjectItemComponent } from './project-item/project-item.component';
import { ConfigurationItemEditComponent } from './configuration-item-edit/configuration-item-edit.component';


@NgModule({
  declarations: [
    UserListComponent,
    UserEditComponent,
    ProjectListComponent,
    NamespaceEditComponent,
    ProjectEditComponent,
    ConfigurationItemListComponent,
    NamespaceListComponent,
    ProjectItemComponent,
    ConfigurationItemEditComponent
  ],
  imports: [
    CommonModule,
    PagesRoutingModule,
    NgZorroAntdModule,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class PagesModule { }
