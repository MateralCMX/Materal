import { Injectable } from '@angular/core';
import { BasiceService } from './BasiceService';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { NzMessageService } from 'ng-zorro-antd';
import { ResultDataModel } from './models/result/resultDataModel';
import { LoginRequestModel } from './models/user/loginRequestModel';
import { AuthorityCommon } from '../components/authority-common';
import { TokenResultModel } from './models/user/TokenResultModel';

@Injectable({
  providedIn: 'root'
})
export class UserService extends BasiceService {
  constructor(protected route: Router, protected http: HttpClient, protected message: NzMessageService,
              protected authorityCommon: AuthorityCommon) {
    super(route, http, message, authorityCommon);
  }
  /**
   * 登录
   */
  public login(data: LoginRequestModel, success?: (value: ResultDataModel<TokenResultModel>) => void, complete?: () => void) {
    return this.sendPost('/User/Login', data, (result: ResultDataModel<TokenResultModel>) => {
      this.authorityCommon.setToken(result.Data);
      success(result);
    }, null, complete);
  }
}
