import { Component, OnInit, EventEmitter } from '@angular/core';
import { ServerCommon } from '../components/server-common';
import { ServerService } from '../services/server.service';
import { ResultDataModel } from '../services/models/result/resultDataModel';
import { ServerListDTO } from '../services/models/server/serverListDTO';
import { Router } from '@angular/router';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class IndexComponent implements OnInit {
  public hasServer = false;
  public selectedValue: string;
  public serverList: ServerListDTO[] = [];
  public constructor(private serverCommon: ServerCommon, private serverService: ServerService, private route: Router) {
    this.loadServerList();
  }
  public ngOnInit() {
  }
  public loadServerList() {
    const success = (result: ResultDataModel<ServerListDTO[]>) => {
      this.serverList = result.Data;
      this.bindSelectedValue();
    };
    this.serverService.getServerList(success);
  }
  public serverListChange() {
    this.setServerUrl(this.selectedValue);
    window.location.reload();
  }
  public setServerUrl(url: string) {
    this.serverCommon.setServerUrl(url);
  }
  private bindSelectedValue() {
    if (this.serverList.length !== null && this.serverList.length !== undefined && this.serverList.length > 0) {
      this.handlerServers();
    } else {
      this.handlerNoServer();
    }
  }
  private handlerServers() {
    if (this.serverCommon.hasServer()) {
      const temp = this.serverCommon.getTrueServerUrl();
      for (const item of this.serverList) {
        if (item.Address === temp) {
          this.selectedValue = temp;
          break;
        }
      }
    }
    if (!this.serverList) {
      this.selectedValue = this.serverList[0].Address;
      this.setServerUrl(this.selectedValue);
    }
    this.hasServer = true;
  }
  private handlerNoServer() {
    if (this.serverCommon.hasServer()) {
      this.serverCommon.removeServerUrl();
    }
    this.route.navigate(['/Index/UserList']);
  }
}
