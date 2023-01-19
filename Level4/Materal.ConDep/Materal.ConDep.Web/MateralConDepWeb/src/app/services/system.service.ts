import { Injectable } from '@angular/core';
import { BasiceService } from './BasiceService';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { NzMessageService } from 'ng-zorro-antd';
import { ResultDataModel } from './models/result/resultDataModel';
import { SystemInfo } from './models/system/SystemInfo';
import { AuthorityCommon } from '../components/authority-common';
import { ResultModel } from './models/result/resultModel';
import { ServerCommon } from '../components/server-common';

@Injectable({
  providedIn: 'root'
})
export class SystemService extends BasiceService {
  constructor(protected route: Router, protected http: HttpClient, protected message: NzMessageService,
              protected authorityCommon: AuthorityCommon, private serverCommon: ServerCommon) {
    super(route, http, message, authorityCommon);
  }
  /**
   * 获取系统信息
   */
  public getSystemInfo(success?: (value: ResultDataModel<SystemInfo>) => void) {
    return this.sendGet('/System/GetSystemInfo', null, success, null, null);
  }
  /**
   * 获取默认数据
   */
  public getDefaultData(success?: (value: ResultDataModel<string>) => void) {
    const url = this.serverCommon.getServerUrl();
    return this.sendGetUrl(url + '/System/GetDefaultData', null, success, null, null);
  }
  /**
   * 设置默认数据
   */
  public setDefaultData(data: string, success?: (value: ResultModel) => void) {
    const url = this.serverCommon.getServerUrl();
    return this.sendPostUrl(url + '/System/SetDefaultData', { Data: data }, success, null, null);
  }
}
