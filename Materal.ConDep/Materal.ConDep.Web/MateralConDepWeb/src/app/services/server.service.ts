import { Injectable } from '@angular/core';
import { BasiceService } from './BasiceService';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { NzMessageService } from 'ng-zorro-antd';
import { AuthorityCommon } from '../components/authority-common';
import { ResultDataModel } from './models/result/resultDataModel';
import { ServerListDTO } from './models/server/serverListDTO';

@Injectable({
  providedIn: 'root'
})
export class ServerService extends BasiceService {
  constructor(protected route: Router, protected http: HttpClient, protected message: NzMessageService,
              protected authorityCommon: AuthorityCommon) {
    super(route, http, message, authorityCommon);
  }
  /**
   * 获取服务信息
   */
  public getServerList(success?: (value: ResultDataModel<ServerListDTO[]>) => void) {
    return this.sendGet('/Server/GetServerList', null, success, null, null);
  }
}
