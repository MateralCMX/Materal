import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ResultModel } from 'src/app/services/models/result/resultModel';
import { AppService } from 'src/app/services/app.service';
import { FormGroupCommon } from 'src/app/components/form-group-common';
import { ResultDataModel } from 'src/app/services/models/result/resultDataModel';
import { WebAppModel } from 'src/app/services/models/app/WebAppModel';

@Component({
  selector: 'app-web-app-edit',
  templateUrl: './web-app-edit.component.html',
  styleUrls: ['./web-app-edit.component.css']
})
export class WebAppEditComponent implements OnInit {
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
      parameters: new FormControl({ value: null, disabled: this.isTransmitting }, [])
    });
  }
  public InitData(appID) {
    this.appID = appID;
    if (!this.appID) {
      this.isAdd = true;
      this.formData.setValue({
        name: null,
        parameters: null
      });
    } else {
      this.isAdd = false;
      this.isTransmitting = true;
      const success = (result: ResultDataModel<WebAppModel>) => {
        this.formData.setValue({
          name: result.Data.Name,
          parameters: result.Data.Parameters
        });
      };
      const complete = () => {
        this.isTransmitting = false;
      };
      this.appService.getWebAppInfo(this.appID, success, complete);
    }
  }
  public save() {
    if (!this.formGroupCommon.canValid(this.formData)) { return; }
    this.isTransmitting = true;
    const data: WebAppModel = {
      ID: this.appID,
      Name: this.formData.value.name,
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
      this.appService.addWebApp(data, success, complete);
    } else {
      this.appService.editWebApp(data, success, complete);
    }
  }
}
