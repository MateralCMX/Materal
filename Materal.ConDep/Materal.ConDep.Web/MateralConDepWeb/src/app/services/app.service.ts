import { Injectable } from '@angular/core';
import { BasiceService } from './BasiceService';
import { Router } from '@angular/router';
import { HttpClient, HttpRequest, HttpEvent, HttpEventType, HttpResponse, HttpHeaders } from '@angular/common/http';
import { NzMessageService, UploadXHRArgs } from 'ng-zorro-antd';
import { AuthorityCommon } from '../components/authority-common';
import { ResultDataModel } from './models/result/resultDataModel';
import { AppListModel } from './models/app/AppListModel';
import { AppModel } from './models/app/appModel';
import { ResultModel } from './models/result/resultModel';
import { WebAppModel } from './models/app/WebAppModel';
import { ServerCommon } from '../components/server-common';

@Injectable({
  providedIn: 'root'
})
export class AppService extends BasiceService {
  constructor(protected route: Router, protected http: HttpClient, protected message: NzMessageService,
              protected authorityCommon: AuthorityCommon, private serverCommon: ServerCommon) {
    super(route, http, message, authorityCommon);
  }
  /**
   * 获得应用列表
   */
  public getAppList(success?: (value: ResultDataModel<AppListModel[]>) => void, complete?: () => void) {
    const url = this.serverCommon.getServerUrl();
    return this.sendGetUrl(url + '/App/GetAppList', null, success, null, complete);
  }
  /**
   * 获得应用信息
   */
  public getAppInfo(id: string, success?: (value: ResultDataModel<AppModel>) => void, complete?: () => void) {
    const url = this.serverCommon.getServerUrl();
    return this.sendGetUrl(url + '/App/GetAppInfo', { id }, success, null, complete);
  }
  /**
   * 添加一个应用
   */
  public addApp(data: AppModel, success?: (value: ResultModel) => void, complete?: () => void) {
    const url = this.serverCommon.getServerUrl();
    return this.sendPostUrl(url + '/App/AddApp', data, success, null, complete);
  }
  /**
   * 修改一个应用
   */
  public editApp(data: AppModel, success?: (value: ResultModel) => void, complete?: () => void) {
    const url = this.serverCommon.getServerUrl();
    return this.sendPostUrl(url + '/App/EditApp', data, success, null, complete);
  }
  /**
   * 删除一个应用
   */
  public deleteApp(id: string, success?: (value: ResultModel) => void, complete?: () => void) {
    const url = this.serverCommon.getServerUrl();
    return this.sendGetUrl(url + '/App/DeleteApp', { id }, success, null, complete);
  }
  /**
   * 获得Web应用列表
   */
  public getWebAppList(success?: (value: ResultDataModel<WebAppModel[]>) => void, complete?: () => void) {
    const url = this.serverCommon.getServerUrl();
    return this.sendGetUrl(url + '/App/GetWebAppList', null, success, null, complete);
  }
  /**
   * 获得Web应用信息
   */
  public getWebAppInfo(id: string, success?: (value: ResultDataModel<WebAppModel>) => void, complete?: () => void) {
    const url = this.serverCommon.getServerUrl();
    return this.sendGetUrl(url + '/App/GetWebAppInfo', { id }, success, null, complete);
  }
  /**
   * 添加一个Web应用
   */
  public addWebApp(data: WebAppModel, success?: (value: ResultModel) => void, complete?: () => void) {
    const url = this.serverCommon.getServerUrl();
    return this.sendPostUrl(url + '/App/AddWebApp', data, success, null, complete);
  }
  /**
   * 修改一个Web应用
   */
  public editWebApp(data: WebAppModel, success?: (value: ResultModel) => void, complete?: () => void) {
    const url = this.serverCommon.getServerUrl();
    return this.sendPostUrl(url + '/App/EditWebApp', data, success, null, complete);
  }
  /**
   * 删除一个Web应用
   */
  public deleteWebApp(id: string, success?: (value: ResultModel) => void, complete?: () => void) {
    const url = this.serverCommon.getServerUrl();
    return this.sendGetUrl(url + '/App/DeleteWebApp', { id }, success, null, complete);
  }
  /**
   * 启动所有应用
   */
  public startAllApp(success?: (value: ResultModel) => void, complete?: () => void) {
    const url = this.serverCommon.getServerUrl();
    return this.sendGetUrl(url + '/App/StartAllApp', null, success, null, complete);
  }
  /**
   * 停止所有应用
   */
  public stopAllApp(success?: (value: ResultModel) => void, complete?: () => void) {
    const url = this.serverCommon.getServerUrl();
    return this.sendGetUrl(url + '/App/StopAllApp', null, success, null, complete);
  }
  /**
   * 启动应用
   */
  public startApp(id: string, success?: (value: ResultModel) => void, complete?: () => void) {
    const url = this.serverCommon.getServerUrl();
    return this.sendGetUrl(url + '/App/StartApp', { id }, success, null, complete);
  }
  /**
   * 停止应用
   */
  public stopApp(id: string, success?: (value: ResultModel) => void, complete?: () => void) {
    const url = this.serverCommon.getServerUrl();
    return this.sendGetUrl(url + '/App/StopApp', { id }, success, null, complete);
  }
  /**
   * 获得控制台列表
   */
  public getConsoleList(id: string, success?: (value: ResultDataModel<string[]>) => void, complete?: () => void) {
    const url = this.serverCommon.getServerUrl();
    return this.sendGetUrl(url + '/App/GetConsoleList', { id }, success, null, complete);
  }
  /**
   * 清理缓存
   */
  public clearCache(success?: (value: ResultDataModel<string[]>) => void, complete?: () => void) {
    const url = this.serverCommon.getServerUrl();
    return this.sendGetUrl(url + '/App/ClearCache', null, success, null, complete);
  }
  /**
   * 上传文件
   */
  public updateAppFile(item: UploadXHRArgs) {
    let url = this.serverCommon.getServerUrl();
    url = `${url}/App/UpdateAppFile`;
    const formData = new FormData();
    formData.append('file', item.file as any);
    const token = this.authorityCommon.getToken();
    (window as any).axios({
      method: 'post',
      url,
      data: formData,
      headers: { Authorization: `Bearer ${token}` }
    }).then(result => {
      item.onSuccess(result.request, item.file, result.request);
    }).catch(error => {
      item.onError(error.request, item.file);
      this.handlerError(error.request);
    });
  }
}
