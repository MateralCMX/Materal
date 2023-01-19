import { Component, OnInit, ViewChild } from '@angular/core';
import { AppService } from 'src/app/services/app.service';
import { ResultDataModel } from 'src/app/services/models/result/resultDataModel';
import { AppListModel } from 'src/app/services/models/app/AppListModel';
import { FormGroup, FormControl } from '@angular/forms';
import { AppEditComponent } from '../app-edit/app-edit.component';
import { ResultModel } from 'src/app/services/models/result/resultModel';
import { NzMessageService, NzModalService } from 'ng-zorro-antd';
import { ServerCommon } from 'src/app/components/server-common';

@Component({
  selector: 'app-app-list',
  templateUrl: './app-list.component.html',
  styleUrls: ['./app-list.component.css']
})
export class AppListComponent implements OnInit {
  @ViewChild('appEditComponent', { static: false })
  public appEditComponent: AppEditComponent;
  public searchModel: FormGroup;
  public listOfData: AppListModel[];
  public listOfDisplayData: AppListModel[];
  public dataLoading = false;
  public sortName: string;
  public sortValue: string;
  public drawerVisible = false;
  public modalVisible = false;
  public generalMessage = {
    count: 0,
    run: 0,
    stop: 0
  };
  public consoleMessages: string[];
  public isAdd = false;
  public constructor(private appService: AppService, private message: NzMessageService, private modalService: NzModalService,
                     private serverCommon: ServerCommon) { }
  public ngOnInit() {
    this.searchModel = new FormGroup({
      name: new FormControl({ value: null, disabled: this.dataLoading }),
      appStatus: new FormControl({ value: null, disabled: this.dataLoading }),
      appPath: new FormControl({ value: null, disabled: this.dataLoading }),
      mainModuleName: new FormControl({ value: null, disabled: this.dataLoading }),
      parameters: new FormControl({ value: null, disabled: this.dataLoading })
    });
    this.awitInit();
  }
  private awitInit() {
    if (this.serverCommon.hasServer()) {
      this.initListData();
    } else {
      setTimeout(() => {
        this.awitInit();
      }, 1000);
    }
  }
  private initListData() {
    this.dataLoading = true;
    const success = (result: ResultDataModel<AppListModel[]>) => {
      this.listOfData = result.Data;
      this.generalMessage = {
        count: 0,
        run: 0,
        stop: 0
      };
      result.Data.forEach(item => {
        this.generalMessage.count++;
        if (item.AppStatus === 0) {
          this.generalMessage.stop++;
        } else {
          this.generalMessage.run++;
        }
      });
      this.search();
    };
    const complete = () => {
      this.dataLoading = false;
    };
    this.appService.getAppList(success, complete);
  }
  public sort(sort: { key: string; value: string }): void {
    this.sortName = sort.key;
    this.sortValue = sort.value;
    this.search();
  }
  public search(): void {
    const data = this.listOfData.filter((item) => this.filterData(item));
    if (this.sortName && this.sortValue) {
      this.listOfDisplayData = data.sort((a, b) => {
        if (this.sortValue === 'ascend') {
          return a[this.sortName] > b[this.sortName] ? 1 : -1;
        } else {
          return b[this.sortName] > a[this.sortName] ? 1 : -1;
        }
      });
    } else {
      this.listOfDisplayData = data;
    }
  }
  private filterData(item: AppListModel) {
    if ((this.searchModel.value.name && !(item.Name.indexOf(this.searchModel.value.name) >= 0)) ||
      (this.searchModel.value.appStatus != null && item.AppStatus !== this.searchModel.value.appStatus) ||
      (this.searchModel.value.appPath && !(item.AppPath.indexOf(this.searchModel.value.appPath) >= 0)) ||
      (this.searchModel.value.mainModuleName && !(item.MainModuleName.indexOf(this.searchModel.value.mainModuleName) >= 0)) ||
      (this.searchModel.value.parameters && !(item.Parameters.indexOf(this.searchModel.value.parameters) >= 0))
    ) {
      return false;
    }
    return true;
  }
  public openDrawer(appid: string): void {
    this.appEditComponent.InitData(appid);
    this.isAdd = this.appEditComponent.isAdd;
    this.drawerVisible = true;
  }
  public closeDrawer(): void {
    this.drawerVisible = false;
  }
  public saveEnd(result: ResultModel) {
    this.message.success(result.Message);
    this.initListData();
    this.closeDrawer();
  }
  public deleteApp(appID: string): void {
    this.dataLoading = true;
    const success = (result: ResultModel) => {
      this.initListData();
      this.message.success(result.Message);
    };
    const complete = () => {
      this.dataLoading = false;
    };
    this.appService.deleteApp(appID, success, complete);
  }
  public getConsoleList(appID: string): void {
    this.dataLoading = true;
    const success = (result: ResultDataModel<string[]>) => {
      this.consoleMessages = result.Data;
      this.modalVisible = true;
    };
    const complete = () => {
      this.dataLoading = false;
    };
    this.appService.getConsoleList(appID, success, complete);
  }
  public StartAllApp(): void {
    this.dataLoading = true;
    const success = (result: ResultModel) => {
      this.initListData();
      this.message.success(result.Message);
    };
    const complete = () => {
      this.dataLoading = false;
    };
    this.appService.startAllApp(success, complete);
  }
  public StartApp(appID: string): void {
    this.dataLoading = true;
    const success = (result: ResultModel) => {
      this.initListData();
      this.message.success(result.Message);
    };
    const complete = () => {
      this.dataLoading = false;
    };
    this.appService.startApp(appID, success, complete);
  }
  public StopAllApp(): void {
    this.dataLoading = true;
    const success = (result: ResultModel) => {
      this.initListData();
      this.message.success(result.Message);
    };
    const complete = () => {
      this.dataLoading = false;
    };
    this.appService.stopAllApp(success, complete);
  }
  public StopApp(appID: string): void {
    this.dataLoading = true;
    const success = (result: ResultModel) => {
      this.initListData();
      this.message.success(result.Message);
    };
    const complete = () => {
      this.dataLoading = false;
    };
    this.appService.stopApp(appID, success, complete);
  }
  public closeModal(): void {
    this.modalVisible = false;
  }
  public showDeleteConfirm(appID: string): void {
    this.modalService.confirm({
      nzTitle: '提示',
      nzContent: '确认删除该项配置吗？',
      nzOkText: '删除',
      nzOkType: 'danger',
      nzOnOk: () => this.deleteApp(appID),
      nzCancelText: '取消'
    });
  }
  public clearCache(): void {
    this.dataLoading = true;
    const success = (result: ResultModel) => {
      this.message.success(result.Message);
    };
    const complete = () => {
      this.dataLoading = false;
    };
    this.appService.clearCache(success, complete);
  }
}
