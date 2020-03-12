import { Injectable } from '@angular/core';
import { BasiceService } from './BasiceService';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { NzMessageService } from 'ng-zorro-antd';
import { ResultDataModel } from './models/result/resultDataModel';
import { LoginRequestModel } from './models/authority/loginRequestModel';
import { AuthorityCommon } from '../components/authority-common';
import { ResultModel } from './models/result/resultModel';

@Injectable({
  providedIn: 'root'
})
export class AuthorityService extends BasiceService {
  constructor(protected route: Router, protected http: HttpClient, protected message: NzMessageService,
              protected authorityCommon: AuthorityCommon) {
    super(route, http, message, authorityCommon);
  }
  /**
   * 登录
   */
  public login(data: LoginRequestModel, success?: (value: ResultDataModel<string>) => void, complete?: () => void) {
    return this.sendPost('/Authority/Login', data, (result: ResultDataModel<string>) => {
      this.authorityCommon.setToken(result.Data);
      success(result);
    }, null, complete);
  }
  /**
   * 登出
   */
  public logout(success?: (value: ResultModel) => void, complete?: () => void) {
    if (this.authorityCommon.getToken()) {
      return this.sendGet('/Authority/Logout', null, (result: ResultModel) => {
        this.authorityCommon.removeToken();
        if (success) {
          success(result);
        }
      }, null, complete);
    }
  }
}
