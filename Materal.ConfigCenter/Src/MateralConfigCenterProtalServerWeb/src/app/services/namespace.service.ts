import { Injectable } from '@angular/core';
import { BasiceService } from './BasiceService';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { NzMessageService } from 'ng-zorro-antd';
import { AuthorityCommon } from '../components/authority-common';
import { EditNamespaceModel } from './models/namespace/EditNamespaceModel';
import { ResultModel } from './models/result/resultModel';
import { ResultDataModel } from './models/result/resultDataModel';
import { NamespaceDTO } from './models/namespace/NamespaceDTO';
import { QueryNamespaceFilterModel } from './models/namespace/QueryNamespaceFilterModel';
import { PageResultModel } from './models/result/pageResultModel';
import { NamespaceListDTO } from './models/namespace/NamespaceListDTO';

@Injectable({
  providedIn: 'root'
})
export class NamespaceService extends BasiceService {
  constructor(protected route: Router, protected http: HttpClient, protected message: NzMessageService,
              protected authorityCommon: AuthorityCommon) {
    super(route, http, message, authorityCommon);
  }
  /**
   * 添加命名空间
   */
  public addNamespace(data: EditNamespaceModel, success?: (value: ResultModel) => void, complete?: () => void) {
    return this.sendPost('/Namespace/AddNamespace', data, success, null, complete);
  }
  /**
   * 修改命名空间
   */
  public editNamespace(data: EditNamespaceModel, success?: (value: ResultModel) => void, complete?: () => void) {
    return this.sendPost('/Namespace/EditNamespace', data, success, null, complete);
  }
  /**
   * 删除命名空间
   */
  public deleteNamespace(id: string, success?: (value: ResultModel) => void, complete?: () => void) {
    return this.sendGet('/Namespace/DeleteNamespace', { id }, success, null, complete);
  }
  /**
   * 获得命名空间信息
   */
  public getNamespaceInfo(id: string, success?: (value: ResultDataModel<NamespaceDTO>) => void, complete?: () => void) {
    return this.sendGet('/Namespace/GetNamespaceInfo', { id }, success, null, complete);
  }
  /**
   * 获得命名空间列表
   */
  public getNamespaceList(data: QueryNamespaceFilterModel, success?: (value: PageResultModel<NamespaceListDTO>) => void, 
                          complete?: () => void) {
    return this.sendPost('/Namespace/GetNamespaceList', data, success, null, complete);
  }
}
