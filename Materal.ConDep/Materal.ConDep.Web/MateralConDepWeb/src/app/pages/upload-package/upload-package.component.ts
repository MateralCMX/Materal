import { Component, OnInit } from '@angular/core';
import { NzMessageService } from 'ng-zorro-antd/message';
import { UploadChangeParam, UploadFilter, UploadFile } from 'ng-zorro-antd/upload';
import { AppService } from 'src/app/services/app.service';

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
  public uploadFilePath: string;
  constructor(private appService: AppService, private msg: NzMessageService) { }

  ngOnInit() {
    this.uploadFilePath = `${this.appService.baseUrl}/App/UploadFile`;
  }
  handleChange({ file, fileList }: UploadChangeParam): void {
    const status = file.status;
    if (status !== 'uploading') {
      console.log(file, fileList);
    }
    if (status === 'done') {
      this.msg.success(`${file.name} file uploaded successfully.`);
    } else if (status === 'error') {
      this.msg.error(`${file.name} file upload failed.`);
    }
  }
}
