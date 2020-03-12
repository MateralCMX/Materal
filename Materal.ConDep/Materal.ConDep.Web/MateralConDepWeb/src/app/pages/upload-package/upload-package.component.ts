import { Component, OnInit } from '@angular/core';
import { NzMessageService } from 'ng-zorro-antd/message';
import { UploadChangeParam, UploadFilter, UploadFile, UploadXHRArgs } from 'ng-zorro-antd/upload';
import { AppService } from 'src/app/services/app.service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-upload-package',
  templateUrl: './upload-package.component.html',
  styleUrls: ['./upload-package.component.css']
})
export class UploadPackageComponent implements OnInit {
  public filters: UploadFilter[] = [
    {
      name: 'type',
      fn: (fileList: UploadFile[]) => {
        const filterFiles = fileList.filter(m => {
          const index = m.name.lastIndexOf('.');
          if (index < 0) { return false; }
          const extension = m.name.substr(index);
          return extension === '.rar';
        });
        if (filterFiles.length !== fileList.length) {
          this.msg.error(`包含文件格式不正确，只支持 rar 格式`);
          return filterFiles;
        }
        return fileList;
      }
    }
  ];
  public constructor(private appService: AppService, private msg: NzMessageService) { }
  public ngOnInit() { }
  public customRequest() {
    return (item: UploadXHRArgs) => {
      this.appService.updateAppFile(item);
    };
  }
  public handleChange({ file, fileList }: UploadChangeParam): void {
    const status = file.status;
    if (status === 'done') {
      this.msg.success(`${file.name} 上传成功`);
    } else if (status === 'error') {
      this.msg.error(`${file.name} 上传失败`);
    }
  }
}
