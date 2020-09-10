import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { SystemService } from '../services/system.service';
import { ResultDataModel } from '../services/models/result/resultDataModel';
import { SystemInfo } from '../services/models/system/SystemInfo';
import { LoginRequestModel } from '../services/models/user/loginRequestModel';
import { UserService } from '../services/user.service';
import { FormGroupCommon } from '../components/form-group-common';
import { NzMessageService } from 'ng-zorro-antd';
import { ServerCommon } from '../components/server-common';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public systemInfo: SystemInfo = {
    Name: 'Materal-ConDep-控制中心',
    Version: '1.0.0'
  };
  public formData: FormGroup;
  public isLoging = false;
  constructor(private router: Router, private systemService: SystemService, private userService: UserService,
              private formGroupCommon: FormGroupCommon, protected message: NzMessageService,
              private serverCommon: ServerCommon) {
  }
  public ngOnInit(): void {
    this.serverCommon.removeServerUrl();
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
    const data: LoginRequestModel = {
      Account: this.formData.value.account,
      Password: this.formData.value.password
    };
    const success = () => {
      this.router.navigate(['/Index/AppList']);
    };
    const complete = () => {
      this.isLoging = false;
    };
    this.userService.login(data, success, complete);
  }
}
