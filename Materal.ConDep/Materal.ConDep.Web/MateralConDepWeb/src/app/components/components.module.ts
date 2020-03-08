import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthorityCommon } from './authority-common';
import { FormGroupCommon } from './form-group-common';

@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  exports: [],
  providers: [AuthorityCommon, FormGroupCommon]
})
export class ComponentsModule { }
