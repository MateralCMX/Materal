import { Component, OnInit, ViewChild } from '@angular/core';
import { ProjectEditComponent } from '../project-edit/project-edit.component';
import { ResultModel } from 'src/app/services/models/result/resultModel';
import { ProjectService } from 'src/app/services/project.service';
import { NzMessageService, NzModalService } from 'ng-zorro-antd';
import { QueryProjectFilterModel } from 'src/app/services/models/project/QueryProjectFilterModel';
import { PageResultModel } from 'src/app/services/models/result/pageResultModel';
import { ProjectListDTO } from 'src/app/services/models/project/ProjectListDTO';
import { FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-project-list',
  templateUrl: './project-list.component.html',
  styleUrls: ['./project-list.component.less']
})
export class ProjectListComponent implements OnInit {
  @ViewChild('projectEditComponent', { static: false })
  public projectEditComponent: ProjectEditComponent;
  public isAdd = true;
  public drawerVisible = false;
  public dataLoading = false;
  public tableData: ProjectListDTO[] = [];
  public searchModel: FormGroup;
  public constructor(private projectService: ProjectService, private message: NzMessageService) { }

  public ngOnInit() {
    this.searchModel = new FormGroup({
      name: new FormControl({ value: null, disabled: this.dataLoading }),
      description: new FormControl({ value: null, disabled: this.dataLoading })
    });
    this.search();
  }
  public openDrawer(appid: string): void {
    this.projectEditComponent.InitData(appid);
    this.isAdd = this.projectEditComponent.isAdd;
    this.drawerVisible = true;
  }
  public search() {
    this.dataLoading = true;
    const data: QueryProjectFilterModel = {
      Name: this.searchModel.value.name,
      Description: this.searchModel.value.description
    };
    const success = (result: PageResultModel<ProjectListDTO>) => {
      this.tableData = result.Data;
    };
    const complete = () => {
      this.dataLoading = false;
    };
    this.projectService.getProjectList(data, success, complete);
  }
  public saveEnd(result: ResultModel) {
    this.message.success(result.Message);
    this.search();
    this.closeDrawer();
  }
  public closeDrawer() {
    this.drawerVisible = false;
  }
  public deleteProject(projectID: string): void {
    this.dataLoading = true;
    const success = (result: ResultModel) => {
      this.message.success(result.Message);
      this.search();
    };
    const complete = () => {
      this.dataLoading = false;
    };
    this.projectService.deleteProject(projectID, success, complete);
  }
}
