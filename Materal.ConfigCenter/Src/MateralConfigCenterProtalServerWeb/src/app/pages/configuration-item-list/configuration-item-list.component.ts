import { Component, OnInit, ViewChild } from '@angular/core';
import { ConfigurationItemEditComponent } from '../configuration-item-edit/configuration-item-edit.component';
import { ConfigurationItemListDTO } from 'src/app/services/models/configurationItem/ConfigurationItemListDTO';
import { FormGroup, FormControl } from '@angular/forms';
import { ProjectListDTO } from 'src/app/services/models/project/ProjectListDTO';
import { ProjectService } from 'src/app/services/project.service';
import { ConfigurationItemService } from 'src/app/services/configuration-item.service';
import { NzMessageService } from 'ng-zorro-antd';
import { QueryProjectFilterModel } from 'src/app/services/models/project/QueryProjectFilterModel';
import { PageResultModel } from 'src/app/services/models/result/pageResultModel';
import { QueryConfigurationItemFilterModel } from 'src/app/services/models/configurationItem/QueryConfigurationItemFilterModel';
import { ResultModel } from 'src/app/services/models/result/resultModel';
import { NamespaceService } from 'src/app/services/namespace.service';
import { QueryNamespaceFilterModel } from 'src/app/services/models/namespace/QueryNamespaceFilterModel';
import { ConfigServerModel } from 'src/app/services/models/configServer/ConfigServerModel';
import { ConfigServerService } from 'src/app/services/config-server.service';
import { ResultDataModel } from 'src/app/services/models/result/resultDataModel';
import { NamespaceListDTO } from 'src/app/services/models/namespace/NamespaceListDTO';
import { CopyConfigServerModel } from 'src/app/services/models/configServer/CopyConfigServerModel';
import { CopyNamespaceModel } from 'src/app/services/models/configServer/CopyNamespaceModel';

@Component({
  selector: 'app-configuration-item-list',
  templateUrl: './configuration-item-list.component.html',
  styleUrls: ['./configuration-item-list.component.less']
})
export class ConfigurationItemListComponent implements OnInit {
  @ViewChild('configurationItemEditComponent', { static: false })
  public configurationItemEditComponent: ConfigurationItemEditComponent;
  public isAdd = true;
  public drawerVisible = false;
  public dataLoading = false;
  public tableData: ConfigurationItemListDTO[] = [];
  public searchModel: FormGroup;
  public selectedProject: ProjectListDTO;
  public selectedNamespace: NamespaceListDTO;
  public projectData: ProjectListDTO[] = [];
  public namespaceData: any = {};
  public configServers: ConfigServerModel[] = [];
  public isCopyConfigServerModalVisible = false;
  public checkConfigServers = [];
  public canCopyConfigServer = false;
  public isCopyNamespace = false;
  public constructor(private projectService: ProjectService, private namespaceService: NamespaceService,
                     private configServiceService: ConfigServerService, private configurationItemService: ConfigurationItemService,
                     private message: NzMessageService) { }
  public ngOnInit() {
    this.searchModel = new FormGroup({
      key: new FormControl({ value: null, disabled: this.dataLoading }),
      configServer: new FormControl({ value: null, disabled: this.dataLoading }),
      description: new FormControl({ value: null, disabled: this.dataLoading })
    });
    this.loadConfigServerData();
  }
  public loadConfigServerData() {
    this.dataLoading = true;
    const success = (result: ResultDataModel<ConfigServerModel[]>) => {
      this.configServers = result.Data;
      if (this.configServers.length > 0) {
        this.searchModel.setValue({
          key: null,
          configServer: this.configServers[0],
          description: null
        });
        this.loadProjectData();
      } else {
        this.dataLoading = false;
      }
    };
    this.configServiceService.getConfigServerList(success);
  }
  public loadProjectData() {
    this.dataLoading = true;
    const data: QueryProjectFilterModel = {
      Name: '',
      Description: ''
    };
    const success = (result: ResultDataModel<ProjectListDTO[]>) => {
      this.projectData = result.Data;
    };
    const complete = () => {
      this.dataLoading = false;
    };
    this.projectService.getProjectList(data, success, complete);
  }
  public openDrawer(id: string): void {
    this.configurationItemEditComponent.InitData(id);
    this.isAdd = this.configurationItemEditComponent.isAdd;
    this.drawerVisible = true;
  }
  public search() {
    this.dataLoading = true;
    const data: QueryConfigurationItemFilterModel = {
      NamespaceID: this.selectedNamespace.ID,
      Key: this.searchModel.value.key,
      Description: this.searchModel.value.description
    };
    const success = (result: PageResultModel<ConfigurationItemListDTO>) => {
      this.tableData = result.Data;
    };
    const complete = () => {
      this.dataLoading = false;
    };
    this.configurationItemService.getConfigurationItemList(this.searchModel.value.configServer.Address, data, success, complete);
  }
  public saveEnd(result: ResultModel) {
    this.message.success(result.Message);
    this.search();
    this.closeDrawer();
  }
  public closeDrawer() {
    this.drawerVisible = false;
  }
  public deleteConfigurationItem(ConfigurationItemID: string): void {
    this.dataLoading = true;
    const success = (result: ResultModel) => {
      this.message.success(result.Message);
      this.search();
    };
    const complete = () => {
      this.dataLoading = false;
    };
    this.configurationItemService.deleteConfigurationItem(this.searchModel.value.configServer.Address,
      ConfigurationItemID, success, complete);
  }
  public selectNamespace() {
    this.search();
  }
  public onProjectActiveChange(event: boolean, project: ProjectListDTO) {
    if (event) {
      this.selectedProject = project;
      if (!this.namespaceData[project.ID]) {
        this.loadNameSpage(project.ID);
      }
    }
  }
  public loadNameSpage(projectID: string) {
    this.dataLoading = true;
    const data: QueryNamespaceFilterModel = {
      ProjectID: projectID,
      Name: '',
      Description: ''
    };
    const success = (result: PageResultModel<ProjectListDTO>) => {
      this.namespaceData[projectID] = result.Data;
    };
    const complete = () => {
      this.dataLoading = false;
    };
    this.namespaceService.getNamespaceList(data, success, complete);
  }
  public onChangeConfigServer() {
    if (this.selectedNamespace) {
      this.search();
    }
  }
  public copyConfigServer() {
    this.dataLoading = true;
    let data: CopyConfigServerModel | CopyNamespaceModel;
    const targetConfigServerNames: string[] = [];
    for (const configServer of this.checkConfigServers) {
      if (configServer.checked) {
        targetConfigServerNames.push(configServer.value.Name);
      }
    }
    const success = (result: ResultModel) => {
      this.message.success(result.Message);
      this.isCopyConfigServerModalVisible = false;
    };
    const complete = () => {
      this.dataLoading = false;
    };
    if (this.isCopyNamespace) {
      data = {
        SourceConfigServerName: this.searchModel.value.configServer.Name,
        TargetConfigServerNames: targetConfigServerNames,
        NamespaceID: this.selectedNamespace.ID
      };
      this.configServiceService.copyNamespace(data, success, complete);
    } else {
      data = {
        SourceConfigServerName: this.searchModel.value.configServer.Name,
        TargetConfigServerNames: targetConfigServerNames
      };
      this.configServiceService.copyConfigServer(data, success, complete);
    }
  }
  public openCopyConfigServer(data: NamespaceListDTO) {
    if (data) {
      this.selectedNamespace = data;
      this.search();
      this.isCopyNamespace = true;
    } else {
      this.isCopyNamespace = false;
    }
    this.checkConfigServers = [];
    this.configServers.forEach(configserver => {
      if (configserver !== this.searchModel.value.configServer) {
        this.checkConfigServers.push({
          label: configserver.Name,
          value: configserver,
          checked: false
        });
      }
    });
    this.isCopyConfigServerModalVisible = true;
  }
  public cancelCopyConfigServer() {
    this.isCopyConfigServerModalVisible = false;
    this.canCopyConfigServer = false;
  }
  public selectCopyConfigServer() {
    let checkedCount = 0;
    for (const configServer of this.checkConfigServers) {
      if (configServer.checked) {
        checkedCount++;
      }
    }
    this.canCopyConfigServer = checkedCount !== 0;
  }
}
