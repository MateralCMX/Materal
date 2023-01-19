import { Component, OnInit } from '@angular/core';
import { SystemService } from 'src/app/services/system.service';
import { ResultDataModel } from 'src/app/services/models/result/resultDataModel';
import { ResultModel } from 'src/app/services/models/result/resultModel';
import { NzMessageService } from 'ng-zorro-antd';

@Component({
  selector: 'app-default-data-setting',
  templateUrl: './default-data-setting.component.html',
  styleUrls: ['./default-data-setting.component.css']
})
export class DefaultDataSettingComponent implements OnInit {
  public isLoading = false;
  public defaultData: string;
  public constructor(private systemService: SystemService, private message: NzMessageService) { }
  public ngOnInit() {
    this.isLoading = true;
    const success = (result: ResultDataModel<string>) => {
      this.defaultData = result.Data;
      this.isLoading = false;
    };
    this.systemService.getDefaultData(success);
  }
  public save() {
    this.isLoading = true;
    const success = (result: ResultModel) => {
      this.message.success(result.Message);
      this.isLoading = false;
    };
    this.systemService.setDefaultData(this.defaultData, success);
  }
}
