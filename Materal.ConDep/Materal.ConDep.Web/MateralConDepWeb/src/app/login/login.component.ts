import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FormGroupCommon } from '../common/formGroupCommon';
import { Router } from '@angular/router';
import { SystemService } from '../services/system.service';
import { ResultDataModel } from '../services/models/result/resultDataModel';
import { SystemInfo } from '../services/models/system/SystemInfo';
import { LoginRequestModel } from '../services/models/authority/loginRequestModel';
import { AuthorityService } from '../services/authority.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public systemInfo: SystemInfo = {
    Name: 'Materal-持续发布系统',
    Version: '1.0'
  };
  public formData: FormGroup;
  public isLoging = false;
  constructor(private formBuilder: FormBuilder, private router: Router, private systemService: SystemService,
              private authorityService: AuthorityService) { }
  public ngOnInit(): void {
    this.formData = this.formBuilder.group({
      password: [null, [Validators.required]]
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
    if (!FormGroupCommon.canValid(this.formData)) { return; }
    this.isLoging = true;
    const data: LoginRequestModel = {
      Password: this.formData.value.password
    };
    const success = () => {
      this.router.navigate(['/Index/AppList']);
    };
    const complete = () => {
      this.isLoging = false;
    };
    this.authorityService.login(data, success, complete);
  }
}
