import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { SystemService } from '../services/system.service';
import { ResultDataModel } from '../services/models/result/resultDataModel';
import { SystemInfo } from '../services/models/system/SystemInfo';
import { FormGroupCommon } from '../components/form-group-common';
import { ResultModel } from '../services/models/result/resultModel';
import { NzMessageService } from 'ng-zorro-antd';
import { UserService } from '../services/user.service';
import { LoginModel} from '../services/models/user/LoginModel';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.less']
})
export class LoginComponent implements OnInit {
  public systemInfo: SystemInfo = {
    Name: 'Materal-持续发布系统',
    Version: '1.0.0'
  };
  public formData: FormGroup;
  public isLoging = false;
  constructor(private router: Router, private systemService: SystemService, private userService: UserService,
              private formGroupCommon: FormGroupCommon, protected message: NzMessageService) {
  }
  public ngOnInit(): void {
    this.formData = new FormGroup({
      account: new FormControl({ value: null, disabled: this.isLoging }, [Validators.required]),
      password: new FormControl({ value: null, disabled: this.isLoging }, [Validators.required])
    });
    this.loadSystemInfo();
  }
  private loadSystemInfo() {
    const success = (result: ResultDataModel<SystemInfo>) => {
      this.systemInfo = result.Data;
    };
    this.systemService.getSystemInfo(success);
  }
  public login(): void {
    if (!this.formGroupCommon.canValid(this.formData)) { return; }
    this.isLoging = true;
    const data: LoginModel = {
      Account: this.formData.value.account,
      Password: this.formData.value.password
    };
    const success = () => {
      this.router.navigate(['/']);
    };
    const complete = () => {
      this.isLoging = false;
    };
    this.userService.login(data, success, complete);
  }
}
