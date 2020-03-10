import { Injectable } from '@angular/core';
import { BasiceService } from './BasiceService';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { NzMessageService } from 'ng-zorro-antd';
import { AuthorityCommon } from '../components/authority-common';
import { ResultDataModel } from './models/result/resultDataModel';
import { AppListModel } from './models/app/AppListModel';
import { AppModel } from './models/app/appModel';
import { ResultModel } from './models/result/resultModel';

@Injectable({
  providedIn: 'root'
})
export class AppService extends BasiceService {
  constructor(protected route: Router, protected http: HttpClient, protected message: NzMessageService,
              protected authorityCommon: AuthorityCommon) {
    super(route, http, message, authorityCommon);
  }
  /**
   * 获得应用列表
   */
  public getAppList(success?: (value: ResultDataModel<AppListModel[]>) => void, complete?: () => void) {
    return this.sendGet('/App/GetAppList', null, success, null, complete);
  }
  /**
   * 获得应用信息
   */
  public GetAppInfo(id: string, success?: (value: ResultDataModel<AppModel>) => void, complete?: () => void) {
    return this.sendGet('/App/GetAppInfo', { id }, success, null, complete);
  }
  /**
   * 启动所有应用
   */
  public StartAllApp(success?: (value: ResultModel) => void, complete?: () => void) {
    return this.sendGet('/App/StartAllApp', null, success, null, complete);
  }
  /**
   * 停止所有应用
   */
  public StopAllApp(success?: (value: ResultModel) => void, complete?: () => void) {
    return this.sendGet('/App/StopAllApp', null, success, null, complete);
  }
  /**
   * 添加一个应用
   */
  public AddApp(data: AppModel, success?: (value: ResultModel) => void, complete?: () => void) {
    return this.sendPost('/App/AddApp', data, success, null, complete);
  }
  /**
   * 修改一个应用
   */
  public EditApp(data: AppModel, success?: (value: ResultModel) => void, complete?: () => void) {
    return this.sendPost('/App/EditApp', data, success, null, complete);
  }
  /**
   * 删除一个应用
   */
  public DeleteApp(id: string, success?: (value: ResultModel) => void, complete?: () => void) {
    return this.sendGet('/App/DeleteApp', { id }, success, null, complete);
  }
  /**
   * 启动应用
   */
  public StartApp(id: string, success?: (value: ResultModel) => void, complete?: () => void) {
    return this.sendGet('/App/StartApp', { id }, success, null, complete);
  }
  /**
   * 停止应用
   */
  public StopApp(id: string, success?: (value: ResultModel) => void, complete?: () => void) {
    return this.sendGet('/App/StopApp', { id }, success, null, complete);
  }
  /**
   * 获得控制台列表
   */
  public GetConsoleList(id: string, success?: (value: ResultDataModel<string[]>) => void, complete?: () => void) {
    return this.sendGet('/App/GetConsoleList', { id }, success, null, complete);
  }
}
