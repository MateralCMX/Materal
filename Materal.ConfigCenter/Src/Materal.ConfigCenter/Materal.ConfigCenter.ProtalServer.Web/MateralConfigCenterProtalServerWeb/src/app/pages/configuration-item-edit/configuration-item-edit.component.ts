import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ResultModel } from 'src/app/services/models/result/resultModel';
import { ConfigurationItemService } from 'src/app/services/configuration-item.service';
import { FormGroupCommon } from 'src/app/components/form-group-common';
import { ResultDataModel } from 'src/app/services/models/result/resultDataModel';
import { ConfigurationItemDTO } from 'src/app/services/models/configurationItem/ConfigurationItemDTO';
import { EditConfigurationItemModel } from 'src/app/services/models/configurationItem/EditConfigurationItemModel';
import { NamespaceListDTO } from 'src/app/services/models/namespace/NamespaceListDTO';
import { ProjectListDTO } from 'src/app/services/models/project/ProjectListDTO';

@Component({
  selector: 'app-configuration-item-edit',
  templateUrl: './configuration-item-edit.component.html',
  styleUrls: ['./configuration-item-edit.component.less']
})
export class ConfigurationItemEditComponent implements OnInit {
  public id: string;
  public isAdd = false;
  public formData: FormGroup;
  public isTransmitting = false;
  @Input()
  public address: string;
  @Input()
  public project: ProjectListDTO;
  @Input()
  public namespace: NamespaceListDTO;
  @Output()
  public saveEnd = new EventEmitter<ResultModel>();
  constructor(private configurationItemService: ConfigurationItemService, private formGroupCommon: FormGroupCommon) { }
  public ngOnInit() {
    this.formData = new FormGroup({
      key: new FormControl({ value: null, disabled: this.isTransmitting }, [Validators.required, Validators.maxLength(100)]),
      value: new FormControl({ value: null, disabled: this.isTransmitting }),
      description: new FormControl({ value: null, disabled: this.isTransmitting }, [Validators.required])
    });
  }
  public InitData(id) {
    this.id = id;
    if (!this.id) {
      this.isAdd = true;
      this.formData.setValue({
        key: null,
        value: null,
        description: null
      });
    } else {
      this.isAdd = false;
      this.isTransmitting = true;
      const success = (result: ResultDataModel<ConfigurationItemDTO>) => {
        this.formData.setValue({
          key: result.Data.Key,
          value: result.Data.Value,
          description: result.Data.Description
        });
      };
      const complete = () => {
        this.isTransmitting = false;
      };
      this.configurationItemService.getConfigurationItemInfo(this.address, this.id, success, complete);
    }
  }
  public save() {
    if (!this.formGroupCommon.canValid(this.formData)) { return; }
    this.isTransmitting = true;
    const data: EditConfigurationItemModel = {
      ID: this.id,
      ProjectID: this.project.ID,
      ProjectName: this.project.Name,
      NamespaceID: this.namespace.ID,
      NamespaceName: this.namespace.Name,
      Key: this.formData.value.key,
      Value: this.formData.value.value,
      Description: this.formData.value.description,
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
      this.configurationItemService.addConfigurationItem(this.address, data, success, complete);
    } else {
      this.configurationItemService.editConfigurationItem(this.address, data, success, complete);
    }
  }
}
