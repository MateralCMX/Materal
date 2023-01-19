import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { AppService } from 'src/app/services/app.service';
import { NzMessageService, NzModalService } from 'ng-zorro-antd';
import { ResultDataModel } from 'src/app/services/models/result/resultDataModel';
import { ResultModel } from 'src/app/services/models/result/resultModel';
import { WebAppModel } from 'src/app/services/models/app/WebAppModel';
import { WebAppEditComponent } from '../web-app-edit/web-app-edit.component';

@Component({
  selector: 'app-web-app-list',
  templateUrl: './web-app-list.component.html',
  styleUrls: ['./web-app-list.component.css']
})
export class WebAppListComponent implements OnInit {
  @ViewChild('webAppEditComponent', { static: false })
  public webAppEditComponent: WebAppEditComponent;
  public searchModel: FormGroup;
  public listOfData: WebAppModel[];
  public listOfDisplayData: WebAppModel[];
  public dataLoading = false;
  public sortName: string;
  public sortValue: string;
  public drawerVisible = false;
  public generalMessage = {
    count: 0
  };
  public isAdd = false;
  public constructor(private appService: AppService, protected message: NzMessageService, private modalService: NzModalService) { }
  public ngOnInit() {
    this.searchModel = new FormGroup({
      name: new FormControl({ value: null, disabled: this.dataLoading }),
      parameters: new FormControl({ value: null, disabled: this.dataLoading })
    });
    this.initListData();
  }
  private initListData() {
    this.dataLoading = true;
    const success = (result: ResultDataModel<WebAppModel[]>) => {
      this.listOfData = result.Data;
      this.generalMessage = {
        count: result.Data.length
      };
      this.search();
    };
    const complete = () => {
      this.dataLoading = false;
    };
    this.appService.getWebAppList(success, complete);
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
  private filterData(item: WebAppModel) {
    if ((this.searchModel.value.name && !(item.Name.indexOf(this.searchModel.value.name) >= 0)) ||
      (this.searchModel.value.parameters && !(item.Parameters.indexOf(this.searchModel.value.parameters) >= 0))
    ) {
      return false;
    }
    return true;
  }
  public openDrawer(appid): void {
    this.webAppEditComponent.InitData(appid);
    this.isAdd = this.webAppEditComponent.isAdd;
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
    this.appService.deleteWebApp(appID, success, complete);
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
}
