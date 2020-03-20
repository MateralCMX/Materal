import { Injectable } from '@angular/core';
import { BasiceService } from './BasiceService';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { NzMessageService } from 'ng-zorro-antd';
import { ResultDataModel } from './models/result/resultDataModel';
import { LoginModel } from './models/user/LoginModel';
import { AuthorityCommon } from '../components/authority-common';
import { ResultModel } from './models/result/resultModel';
import { TokenResultModel } from './models/user/TokenResultModel';
import { EditUserModel } from './models/user/EditUserModel';
import { UserDTO } from './models/user/UserDTO';
import { UserListDTO } from './models/user/UserListDTO';
import { PageResultModel } from './models/result/pageResultModel';
import { QueryUserFilterModel } from './models/user/QueryUserFilterModel';

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
  public login(data: LoginModel, success?: (value: ResultDataModel<TokenResultModel>) => void, complete?: () => void) {
    return this.sendPost('/User/Login', data, (result: ResultDataModel<TokenResultModel>) => {
      this.authorityCommon.setToken(result.Data.AccessToken);
      success(result);
    }, null, complete);
  }
  /**
   * 添加用户
   */
  public addUser(data: EditUserModel, success?: (value: ResultModel) => void, complete?: () => void) {
    return this.sendPost('/User/AddUser', data, success, null, complete);
  }
  /**
   * 修改用户
   */
  public editUser(data: EditUserModel, success?: (value: ResultModel) => void, complete?: () => void) {
    return this.sendPost('/User/EditUser', data, success, null, complete);
  }
  /**
   * 删除用户
   */
  public deleteUser(id: string, success?: (value: ResultModel) => void, complete?: () => void) {
    return this.sendGet('/User/DeleteUser', { id }, success, null, complete);
  }
  /**
   * 获得用户信息
   */
  public getUserInfo(id: string, success?: (value: ResultDataModel<UserDTO>) => void, complete?: () => void) {
    return this.sendGet('/User/GetUserInfo', { id }, success, null, complete);
  }
  /**
   * 获得登录用户信息
   */
  public getLoginUserInfo(success?: (value: ResultDataModel<UserDTO>) => void, complete?: () => void) {
    return this.sendGet('/User/GetLoginUserInfo', null, success, null, complete);
  }
  /**
   * 获得用户列表
   */
  public getUserList(data: QueryUserFilterModel, success?: (value: PageResultModel<UserListDTO>) => void, complete?: () => void) {
    return this.sendPost('/User/GetUserList', data, success, null, complete);
  }
}
