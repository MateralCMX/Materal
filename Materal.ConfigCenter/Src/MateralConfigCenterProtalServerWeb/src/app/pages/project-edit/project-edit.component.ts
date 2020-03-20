import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { ResultModel } from 'src/app/services/models/result/resultModel';
import { FormGroupCommon } from 'src/app/components/form-group-common';
import { ResultDataModel } from 'src/app/services/models/result/resultDataModel';
import { ProjectService } from 'src/app/services/project.service';
import { ProjectDTO } from 'src/app/services/models/project/ProjectDTO';
import { EditProjectModel } from 'src/app/services/models/project/EditProjectModel';

@Component({
  selector: 'app-project-edit',
  templateUrl: './project-edit.component.html',
  styleUrls: ['./project-edit.component.less']
})
export class ProjectEditComponent implements OnInit {
  public appID: string;
  public isAdd = false;
  public formData: FormGroup;
  public isTransmitting = false;
  @Output()
  public saveEnd = new EventEmitter<ResultModel>();
  constructor(private projectService: ProjectService, private formGroupCommon: FormGroupCommon) { }
  public ngOnInit() {
    this.formData = new FormGroup({
      name: new FormControl({ value: null, disabled: this.isTransmitting }, [Validators.required, Validators.maxLength(100)]),
      description: new FormControl({ value: null, disabled: this.isTransmitting }, [Validators.required])
    });
  }
  public InitData(appID) {
    this.appID = appID;
    if (!this.appID) {
      this.isAdd = true;
      this.formData.setValue({
        name: null,
        description: null
      });
    } else {
      this.isAdd = false;
      this.isTransmitting = true;
      const success = (result: ResultDataModel<ProjectDTO>) => {
        this.formData.setValue({
          name: result.Data.Name,
          description: result.Data.Description
        });
      };
      const complete = () => {
        this.isTransmitting = false;
      };
      this.projectService.getProjectInfo(this.appID, success, complete);
    }
  }
  public save() {
    if (!this.formGroupCommon.canValid(this.formData)) { return; }
    this.isTransmitting = true;
    const data: EditProjectModel = {
      ID: this.appID,
      Name: this.formData.value.name,
      Description: this.formData.value.description
    };
    const success = (result: ResultModel) => {
      if (this.saveEnd) {
        this.saveEnd.emit(result);
      }
    };
    const complete = () => {
      this.isTransmitting = false;
    };
    if (this.isAdd) {
      this.projectService.addProject(data, success, complete);
    } else {
      this.projectService.editProject(data, success, complete);
    }
  }
}
