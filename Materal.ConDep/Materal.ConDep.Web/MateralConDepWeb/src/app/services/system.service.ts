import { Injectable } from '@angular/core';
import { BasiceService } from './BasiceService';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { NzMessageService } from 'ng-zorro-antd';
import { ResultDataModel } from './models/result/resultDataModel';
import { SystemInfo } from './models/system/SystemInfo';
import { AuthorityCommon } from '../components/authority-common';
import { ResultModel } from './models/result/resultModel';

@Injectable({
  providedIn: 'root'
})
export class SystemService extends BasiceService {
  constructor(protected route: Router, protected http: HttpClient, protected message: NzMessageService,
              protected authorityCommon: AuthorityCommon) {
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
    return this.sendGet('/System/GetDefaultData', null, success, null, null);
  }
  /**
   * 设置默认数据
   */
  public setDefaultData(data: string, success?: (value: ResultModel) => void) {
    return this.sendPost('/System/SetDefaultData', { Data: data }, success, null, null);
  }
}
