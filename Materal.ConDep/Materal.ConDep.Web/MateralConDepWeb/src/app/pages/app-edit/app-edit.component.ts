import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AppService } from 'src/app/services/app.service';
import { AppModel } from 'src/app/services/models/app/appModel';
import { ResultDataModel } from 'src/app/services/models/result/resultDataModel';
import { FormGroupCommon } from 'src/app/components/form-group-common';
import { ResultModel } from 'src/app/services/models/result/resultModel';

@Component({
  selector: 'app-app-edit',
  templateUrl: './app-edit.component.html',
  styleUrls: ['./app-edit.component.css']
})
export class AppEditComponent implements OnInit {
  public appID: string;
  public isAdd = false;
  public formData: FormGroup;
  public isTransmitting = false;
  @Output()
  public saveEnd = new EventEmitter<ResultModel>();
  constructor(private appService: AppService, private formGroupCommon: FormGroupCommon) { }
  ngOnInit() {
    this.formData = new FormGroup({
      name: new FormControl({ value: null, disabled: this.isTransmitting }, [Validators.required]),
      appPath: new FormControl({ value: null, disabled: this.isTransmitting }, [Validators.required]),
      mainModuleName: new FormControl({ value: null, disabled: this.isTransmitting }, [Validators.required]),
      parameters: new FormControl({ value: null, disabled: this.isTransmitting }, [Validators.required])
    });
  }
  public InitData(appID) {
    this.appID = appID;
    if (!this.appID) {
      this.isAdd = true;
      this.formData.setValue({
        name: null,
        appPath: null,
        mainModuleName: null,
        parameters: null
      });
    } else {
      this.isAdd = false;
      this.isTransmitting = true;
      const success = (result: ResultDataModel<AppModel>) => {
        this.formData.setValue({
          name: result.Data.Name,
          appPath: result.Data.AppPath,
          mainModuleName: result.Data.MainModuleName,
          parameters: result.Data.Parameters
        });
      };
      const complete = () => {
        this.isTransmitting = false;
      };
      this.appService.GetAppInfo(this.appID, success, complete);
    }
  }
  public save() {
    if (!this.formGroupCommon.canValid(this.formData)) { return; }
    this.isTransmitting = true;
    const data: AppModel = {
      ID: this.appID,
      Name: this.formData.value.name,
      AppPath: this.formData.value.appPath,
      MainModuleName: this.formData.value.mainModuleName,
      Parameters: this.formData.value.parameters
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
      this.appService.AddApp(data, success, complete);
    } else {
      this.appService.EditApp(data, success, complete);
    }
  }
}
