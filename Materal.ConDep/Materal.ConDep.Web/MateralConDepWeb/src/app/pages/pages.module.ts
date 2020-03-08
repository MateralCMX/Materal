import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NgZorroAntdModule } from 'ng-zorro-antd';
import { PagesRoutingModule } from './pages-routing.module';
import { AppListComponent } from './app-list/app-list.component';
import { UploadPackageComponent } from './upload-package/upload-package.component';
import { SystemSettingComponent } from './system-setting/system-setting.component';


@NgModule({
  declarations: [AppListComponent, UploadPackageComponent, SystemSettingComponent],
  imports: [
    CommonModule,
    PagesRoutingModule,
    NgZorroAntdModule
  ]
})
export class PagesModule { }
