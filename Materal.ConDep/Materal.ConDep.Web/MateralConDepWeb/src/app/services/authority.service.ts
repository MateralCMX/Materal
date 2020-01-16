import { Injectable } from '@angular/core';
import { BasiceService } from './BasiceService';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { NzMessageService } from 'ng-zorro-antd';
import { ResultDataModel } from './models/result/resultDataModel';
import { LoginRequestModel } from './models/authority/loginRequestModel';
import { AuthorityCommon } from '../common/authorityCommon';

@Injectable({
  providedIn: 'root'
})
export class AuthorityService extends BasiceService {
  constructor(protected route: Router, protected http: HttpClient, protected message: NzMessageService) {
    super(route, http, message);
  }
  /**
   * 登录
   */
  public login(data: LoginRequestModel, success?: (value: ResultDataModel<string>) => void, complete?: () => void) {
    return this.sendPost('/Authority/Login', data, (result: ResultDataModel<string>) => {
      AuthorityCommon.setToken(result.Data);
      success(result);
    }, null, complete);
  }
}
