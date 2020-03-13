import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppListComponent } from './app-list/app-list.component';
import { UploadPackageComponent } from './upload-package/upload-package.component';
import { SystemSettingComponent } from './system-setting/system-setting.component';
import { WebAppListComponent } from './web-app-list/web-app-list.component';


const routes: Routes = [
  { path: 'AppList', component: AppListComponent },
  { path: 'WebAppList', component: WebAppListComponent },
  { path: 'UploadPackage', component: UploadPackageComponent },
  { path: 'SystemSetting', component: SystemSettingComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PagesRoutingModule { }
