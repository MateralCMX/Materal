import { Injectable } from '@angular/core';
import { BasiceService } from './BasiceService';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { NzMessageService } from 'ng-zorro-antd';
import { AuthorityCommon } from '../components/authority-common';
import { ResultModel } from './models/result/resultModel';
import { ResultDataModel } from './models/result/resultDataModel';
import { PageResultModel } from './models/result/pageResultModel';
import { EditConfigurationItemModel } from './models/configurationItem/EditConfigurationItemModel';
import { ConfigurationItemDTO } from './models/configurationItem/ConfigurationItemDTO';
import { QueryConfigurationItemFilterModel } from './models/configurationItem/QueryConfigurationItemFilterModel';
import { ConfigurationItemListDTO } from './models/configurationItem/ConfigurationItemListDTO';

@Injectable({
  providedIn: 'root'
})
export class ConfigurationItemService extends BasiceService {
  constructor(protected route: Router, protected http: HttpClient, protected message: NzMessageService,
              protected authorityCommon: AuthorityCommon) {
    super(route, http, message, authorityCommon);
  }
  /**
   * 添加配置项
   */
  public addConfigurationItem(address: string, data: EditConfigurationItemModel,
                              success?: (value: ResultModel) => void, complete?: () => void) {
    return this.sendPostUrl(`${address}api/ConfigurationItem/AddConfigurationItem`, data, success, null, complete);
  }
  /**
   * 修改配置项
   */
  public editConfigurationItem(address: string, data: EditConfigurationItemModel,
                               success?: (value: ResultModel) => void, complete?: () => void) {
    return this.sendPostUrl(`${address}api/ConfigurationItem/EditConfigurationItem`, data, success, null, complete);
  }
  /**
   * 删除配置项
   */
  public deleteConfigurationItem(address: string, id: string, success?: (value: ResultModel) => void, complete?: () => void) {
    return this.sendGetUrl(`${address}api/ConfigurationItem/DeleteConfigurationItem`, { id }, success, null, complete);
  }
  /**
   * 获得配置项信息
   */
  public getConfigurationItemInfo(address: string, id: string,
                                  success?: (value: ResultDataModel<ConfigurationItemDTO>) => void, complete?: () => void) {
    return this.sendGetUrl(`${address}api/ConfigurationItem/GetConfigurationItemInfo`, { id }, success, null, complete);
  }
  /**
   * 获得配置项列表
   */
  public getConfigurationItemList(address: string, data: QueryConfigurationItemFilterModel,
                                  success?: (value: PageResultModel<ConfigurationItemListDTO>) => void, complete?: () => void) {
    return this.sendPostUrl(`${address}api/ConfigurationItem/GetConfigurationItemList`, data, success, null, complete);
  }
}
