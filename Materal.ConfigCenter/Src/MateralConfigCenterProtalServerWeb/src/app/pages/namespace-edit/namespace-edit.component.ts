import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ResultModel } from 'src/app/services/models/result/resultModel';
import { NamespaceService } from 'src/app/services/namespace.service';
import { FormGroupCommon } from 'src/app/components/form-group-common';
import { ResultDataModel } from 'src/app/services/models/result/resultDataModel';
import { NamespaceDTO } from 'src/app/services/models/namespace/NamespaceDTO';
import { EditNamespaceModel } from 'src/app/services/models/namespace/EditNamespaceModel';

@Component({
  selector: 'app-namespace-edit',
  templateUrl: './namespace-edit.component.html',
  styleUrls: ['./namespace-edit.component.less']
})
export class NamespaceEditComponent implements OnInit {
  public appID: string;
  public isAdd = false;
  public formData: FormGroup;
  public isTransmitting = false;
  @Input()
  public projectID: string;
  @Output()
  public saveEnd = new EventEmitter<ResultModel>();
  constructor(private namespaceService: NamespaceService, private formGroupCommon: FormGroupCommon) { }
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
      const success = (result: ResultDataModel<NamespaceDTO>) => {
        this.formData.setValue({
          name: result.Data.Name,
          description: result.Data.Description
        });
      };
      const complete = () => {
        this.isTransmitting = false;
      };
      this.namespaceService.getNamespaceInfo(this.appID, success, complete);
    }
  }
  public save() {
    if (!this.formGroupCommon.canValid(this.formData)) { return; }
    this.isTransmitting = true;
    const data: EditNamespaceModel = {
      ID: this.appID,
      Name: this.formData.value.name,
      Description: this.formData.value.description,
      ProjectID: this.projectID
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
      this.namespaceService.addNamespace(data, success, complete);
    } else {
      this.namespaceService.editNamespace(data, success, complete);
    }
  }
}
