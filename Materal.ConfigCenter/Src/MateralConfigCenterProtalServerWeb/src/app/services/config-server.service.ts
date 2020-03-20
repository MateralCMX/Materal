import { Injectable } from '@angular/core';
import { BasiceService } from './BasiceService';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { NzMessageService } from 'ng-zorro-antd';
import { AuthorityCommon } from '../components/authority-common';
import { ResultDataModel } from './models/result/resultDataModel';
import { ConfigServerModel } from './models/configServer/ConfigServerModel';
import { CopyConfigServerModel } from './models/configServer/CopyConfigServerModel';
import { ResultModel } from './models/result/resultModel';
import { CopyNamespaceModel } from './models/configServer/CopyNamespaceModel';

@Injectable({
  providedIn: 'root'
})
export class ConfigServerService extends BasiceService {
  constructor(protected route: Router, protected http: HttpClient, protected message: NzMessageService,
              protected authorityCommon: AuthorityCommon) {
    super(route, http, message, authorityCommon);
  }
  /**
   * 获得客户端列表
   */
  public getConfigServerList(success?: (value: ResultDataModel<ConfigServerModel[]>) => void) {
    return this.sendGet('/ConfigServer/GetConfigServerList', null, success, null, null);
  }
  /**
   * 复制配置服务
   */
  public copyConfigServer(data: CopyConfigServerModel, success?: (value: ResultModel) => void, complete?: () => void) {
    return this.sendPost('/ConfigServer/CopyConfigServer', data, success, null, complete);
  }
  /**
   * 复制命名空间
   */
  public copyNamespace(data: CopyNamespaceModel, success?: (value: ResultModel) => void, complete?: () => void) {
    return this.sendPost('/ConfigServer/CopyNamespace', data, success, null, complete);
  }
}
