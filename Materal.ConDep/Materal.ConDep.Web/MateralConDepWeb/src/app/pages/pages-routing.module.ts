import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppListComponent } from './app-list/app-list.component';
import { UploadPackageComponent } from './upload-package/upload-package.component';
import { WebAppListComponent } from './web-app-list/web-app-list.component';
import { DefaultDataSettingComponent } from './default-data-setting/default-data-setting.component';
import { UserListComponent } from './user-list/user-list.component';


const routes: Routes = [
  { path: 'AppList', component: AppListComponent },
  { path: 'WebAppList', component: WebAppListComponent },
  { path: 'UploadPackage', component: UploadPackageComponent },
  { path: 'DefaultDataSetting', component: DefaultDataSettingComponent },
  { path: 'UserList', component: UserListComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PagesRoutingModule { }
