import { Component, OnInit, ViewChild } from '@angular/core';
import { NamespaceEditComponent } from '../namespace-edit/namespace-edit.component';
import { NamespaceListDTO } from 'src/app/services/models/namespace/NamespaceListDTO';
import { FormGroup, FormControl } from '@angular/forms';
import { NamespaceService } from 'src/app/services/namespace.service';
import { NzMessageService } from 'ng-zorro-antd';
import { QueryNamespaceFilterModel } from 'src/app/services/models/namespace/QueryNamespaceFilterModel';
import { PageResultModel } from 'src/app/services/models/result/pageResultModel';
import { ResultModel } from 'src/app/services/models/result/resultModel';
import { ProjectListDTO } from 'src/app/services/models/project/ProjectListDTO';
import { ProjectService } from 'src/app/services/project.service';
import { QueryProjectFilterModel } from 'src/app/services/models/project/QueryProjectFilterModel';
import { ActivatedRoute } from '@angular/router';
import { ResultDataModel } from 'src/app/services/models/result/resultDataModel';

@Component({
  selector: 'app-namespace-list',
  templateUrl: './namespace-list.component.html',
  styleUrls: ['./namespace-list.component.less']
})
export class NamespaceListComponent implements OnInit {
  @ViewChild('namespaceEditComponent', { static: false })
  public namespaceEditComponent: NamespaceEditComponent;
  public isAdd = true;
  public drawerVisible = false;
  public dataLoading = false;
  public tableData: NamespaceListDTO[] = [];
  public searchModel: FormGroup;
  public selectProjectID: string;
  public projectData: ProjectListDTO[] = [];
  public constructor(private projectService: ProjectService, private namespaceService: NamespaceService,
                     private message: NzMessageService, private route: ActivatedRoute) { }
  public ngOnInit() {
    this.searchModel = new FormGroup({
      name: new FormControl({ value: null, disabled: this.dataLoading }),
      description: new FormControl({ value: null, disabled: this.dataLoading })
    });
    this.loadProjectData(this.route.snapshot.paramMap.get('id'));
  }
  public loadProjectData(id: string) {
    this.dataLoading = true;
    const data: QueryProjectFilterModel = {
      Name: '',
      Description: ''
    };
    const success = (result: PageResultModel<ProjectListDTO>) => {
      this.projectData = result.Data;
      if (id) {
        this.selectProjectID = id;
        this.search();
      } else if (this.projectData.length > 0) {
        this.selectProjectID = this.projectData[0].ID;
        this.search();
      } else {
        this.dataLoading = false;
      }
    };
    this.projectService.getProjectList(data, success, null);
  }
  public openDrawer(appid: string): void {
    this.namespaceEditComponent.InitData(appid);
    this.isAdd = this.namespaceEditComponent.isAdd;
    this.drawerVisible = true;
  }
  public search() {
    this.dataLoading = true;
    const data: QueryNamespaceFilterModel = {
      Name: this.searchModel.value.name,
      ProjectID: this.selectProjectID,
      Description: this.searchModel.value.description
    };
    const success = (result: ResultDataModel<NamespaceListDTO[]>) => {
      this.tableData = result.Data;
    };
    const complete = () => {
      this.dataLoading = false;
    };
    this.namespaceService.getNamespaceList(data, success, complete);
  }
  public saveEnd(result: ResultModel) {
    this.message.success(result.Message);
    this.search();
    this.closeDrawer();
  }
  public closeDrawer() {
    this.drawerVisible = false;
  }
  public deleteNamespace(namespaceID: string): void {
    this.dataLoading = true;
    const success = (result: ResultModel) => {
      this.message.success(result.Message);
      this.search();
    };
    const complete = () => {
      this.dataLoading = false;
    };
    this.namespaceService.deleteNamespace(namespaceID, success, complete);
  }
  public selectProject() {
    this.search();
  }
}
