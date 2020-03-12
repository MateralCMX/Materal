import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppListComponent } from './app-list/app-list.component';
import { UploadPackageComponent } from './upload-package/upload-package.component';
import { SystemSettingComponent } from './system-setting/system-setting.component';
import { LocationStrategy, HashLocationStrategy } from '@angular/common';


const routes: Routes = [
  { path: 'AppList', component: AppListComponent },
  { path: 'UploadPackage', component: UploadPackageComponent },
  { path: 'SystemSetting', component: SystemSettingComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: [{ provide: LocationStrategy, useClass: HashLocationStrategy }]
})
export class PagesRoutingModule { }
