import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {

  public searchModel: FormGroup;
  public listOfDisplayData: any[];
  public isAdd = false;
  public drawerVisible = false;
  public dataLoading = false;
  public constructor(private userService: UserService) {

   }

  public ngOnInit() {
    this.searchModel = new FormGroup({
      name: new FormControl({ value: null, disabled: this.dataLoading }),
      account: new FormControl({ value: null, disabled: this.dataLoading })
    });
  }
  public openDrawer(appid: string): void {
    // this.appEditComponent.InitData(appid);
    // this.isAdd = this.appEditComponent.isAdd;
    this.drawerVisible = true;
  }
  public closeDrawer(): void {
    this.drawerVisible = false;
  }
  public search(): void {
  }
}
