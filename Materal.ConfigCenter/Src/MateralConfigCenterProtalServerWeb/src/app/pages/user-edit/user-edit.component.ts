import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ResultModel } from 'src/app/services/models/result/resultModel';
import { UserService } from 'src/app/services/user.service';
import { FormGroupCommon } from 'src/app/components/form-group-common';
import { ResultDataModel } from 'src/app/services/models/result/resultDataModel';
import { UserDTO } from 'src/app/services/models/user/UserDTO';
import { EditUserModel } from 'src/app/services/models/user/EditUserModel';

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.less']
})
export class UserEditComponent implements OnInit {
  public appID: string;
  public isAdd = false;
  public formData: FormGroup;
  public isTransmitting = false;
  @Output()
  public saveEnd = new EventEmitter<ResultModel>();
  constructor(private userService: UserService, private formGroupCommon: FormGroupCommon) { }
  public ngOnInit() {
    this.formData = new FormGroup({
      account: new FormControl({ value: null, disabled: this.isTransmitting }, [Validators.required, Validators.maxLength(100)]),
      password: new FormControl({ value: null, disabled: this.isTransmitting })
    });
  }
  public InitData(appID) {
    this.appID = appID;
    if (!this.appID) {
      this.isAdd = true;
      this.formData.setValue({
        account: null,
        password: null
      });
    } else {
      this.isAdd = false;
      this.isTransmitting = true;
      const success = (result: ResultDataModel<UserDTO>) => {
        this.formData.setValue({
          account: result.Data.Account,
          password: null
        });
      };
      const complete = () => {
        this.isTransmitting = false;
      };
      this.userService.getUserInfo(this.appID, success, complete);
    }
  }
  public save() {
    if (!this.formGroupCommon.canValid(this.formData)) { return; }
    this.isTransmitting = true;
    const data: EditUserModel = {
      ID: this.appID,
      Account: this.formData.value.account,
      Password: this.formData.value.password
    };
    const success = (result: ResultModel) => {
      if (this.saveEnd) {
        this.saveEnd.emit(result);
      }
    };
    const complete = () => {
      this.isTransmitting = false;
    };
    if (this.isAdd) {
      this.userService.addUser(data, success, complete);
    } else {
      this.userService.editUser(data, success, complete);
    }
  }
}
