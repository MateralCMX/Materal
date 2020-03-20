import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { UserListDTO } from 'src/app/services/models/user/UserListDTO';
import { UserService } from 'src/app/services/user.service';
import { PageResultModel } from 'src/app/services/models/result/pageResultModel';
import { QueryUserFilterModel } from 'src/app/services/models/user/QueryUserFilterModel';
import { PageModel } from 'src/app/services/models/result/pageModel';
import { UserEditComponent } from '../user-edit/user-edit.component';
import { ResultModel } from 'src/app/services/models/result/resultModel';
import { NzMessageService, NzModalService } from 'ng-zorro-antd';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.less']
})
export class UserListComponent implements OnInit {
  @ViewChild('userEditComponent', { static: false })
  public userEditComponent: UserEditComponent;
  public searchModel: FormGroup;
  public dataLoading = false;
  public tableData: UserListDTO[] = [];
  public pageModel: PageModel = {
    DataCount: 0,
    PageCount: 0,
    PageIndex: 1,
    PageSize: 2
  };
  public isAdd = false;
  public drawerVisible = false;
  public constructor(private userService: UserService, private message: NzMessageService) { }

  public ngOnInit() {
    this.searchModel = new FormGroup({
      account: new FormControl({ value: null, disabled: this.dataLoading })
    });
    this.search(1);
  }
  public openDrawer(appid: string): void {
    this.userEditComponent.InitData(appid);
    this.isAdd = this.userEditComponent.isAdd;
    this.drawerVisible = true;
  }
  public search(index) {
    this.dataLoading = true;
    const data: QueryUserFilterModel = {
      Account: this.searchModel.value.account,
      PageIndex: index,
      PageSize: this.pageModel.PageSize
    };
    const success = (result: PageResultModel<UserListDTO>) => {
      this.tableData = result.Data;
      this.pageModel = result.PageModel;
    };
    const complete = () => {
      this.dataLoading = false;
    };
    this.userService.getUserList(data, success, complete);
  }
  public closeDrawer() {
    this.drawerVisible = false;
  }
  public saveEnd(result: ResultModel) {
    this.message.success(result.Message);
    this.search(this.pageModel.PageIndex);
    this.closeDrawer();
  }
  public deleteUser(userID: string): void {
    this.dataLoading = true;
    const success = (result: ResultModel) => {
      if (this.pageModel.PageIndex === this.pageModel.PageCount &&  this.pageModel.DataCount % this.pageModel.PageSize === 1) {
        this.search(this.pageModel.PageIndex - 1);
      } else {
        this.search(this.pageModel.PageIndex);
      }
      this.message.success(result.Message);
    };
    const complete = () => {
      this.dataLoading = false;
    };
    this.userService.deleteUser(userID, success, complete);
  }
  public onPageChange(index) {
    this.search(index);
  }
}
