import { Component, OnInit, EventEmitter } from '@angular/core';
import { ServerCommon } from '../components/server-common';
import { ServerService } from '../services/server.service';
import { ResultDataModel } from '../services/models/result/resultDataModel';
import { ServerListDTO } from '../services/models/server/serverListDTO';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class IndexComponent implements OnInit {
  public selectedValue: string;
  public serverList: ServerListDTO[] = [];
  public constructor(private serverCommon: ServerCommon, private serverService: ServerService) {
    this.loadServerList();
  }
  public ngOnInit() {
  }
  public loadServerList() {
    const success = (result: ResultDataModel<ServerListDTO[]>) => {
      this.serverList = result.Data;
      if (this.serverCommon.hasServer()) {
        this.selectedValue = this.serverCommon.getTrueServerUrl();
      } else {
        this.selectedValue = this.serverList[0].Address;
        this.setServerUrl(this.selectedValue);
      }
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
}
