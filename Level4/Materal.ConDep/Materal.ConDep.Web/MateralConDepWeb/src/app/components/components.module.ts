import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthorityCommon } from './authority-common';
import { FormGroupCommon } from './form-group-common';
import { NgZorroAntdModule } from 'ng-zorro-antd';

@NgModule({
  declarations: [],
  imports: [
    NgZorroAntdModule,
    CommonModule
  ],
  exports: [],
  providers: [AuthorityCommon, FormGroupCommon]
})
export class ComponentsModule { }
