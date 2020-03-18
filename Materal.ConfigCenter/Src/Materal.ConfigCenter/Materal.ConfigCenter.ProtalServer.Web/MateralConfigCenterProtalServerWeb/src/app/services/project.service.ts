import { Injectable } from '@angular/core';
import { BasiceService } from './BasiceService';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { NzMessageService } from 'ng-zorro-antd';
import { AuthorityCommon } from '../components/authority-common';
import { ResultModel } from './models/result/resultModel';
import { ResultDataModel } from './models/result/resultDataModel';
import { PageResultModel } from './models/result/pageResultModel';
import { EditProjectModel } from './models/project/EditProjectModel';
import { ProjectDTO } from './models/project/ProjectDTO';
import { QueryProjectFilterModel } from './models/project/QueryProjectFilterModel';
import { ProjectListDTO } from './models/project/ProjectListDTO';

@Injectable({
  providedIn: 'root'
})
export class ProjectService extends BasiceService {
  constructor(protected route: Router, protected http: HttpClient, protected message: NzMessageService,
              protected authorityCommon: AuthorityCommon) {
    super(route, http, message, authorityCommon);
  }
  /**
   * 添加项目
   */
  public addProject(data: EditProjectModel, success?: (value: ResultModel) => void, complete?: () => void) {
    return this.sendPost('/Project/AddProject', data, success, null, complete);
  }
  /**
   * 修改项目
   */
  public editProject(data: EditProjectModel, success?: (value: ResultModel) => void, complete?: () => void) {
    return this.sendPost('/Project/EditProject', data, success, null, complete);
  }
  /**
   * 删除项目
   */
  public deleteProject(id: string, success?: (value: ResultModel) => void, complete?: () => void) {
    return this.sendGet('/Project/DeleteProject', { id }, success, null, complete);
  }
  /**
   * 获得项目信息
   */
  public getProjectInfo(id: string, success?: (value: ResultDataModel<ProjectDTO>) => void, complete?: () => void) {
    return this.sendGet('/Project/GetProjectInfo', { id }, success, null, complete);
  }
  /**
   * 获得项目列表
   */
  public getProjectList(data: QueryProjectFilterModel, success?: (value: PageResultModel<ProjectListDTO>) => void, complete?: () => void) {
    return this.sendPost('/Project/GetProjectList', data, success, null, complete);
  }
}
