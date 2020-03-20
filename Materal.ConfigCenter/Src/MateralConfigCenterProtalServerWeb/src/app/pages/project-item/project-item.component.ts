import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ProjectListDTO } from 'src/app/services/models/project/ProjectListDTO';

@Component({
  selector: 'app-project-item',
  templateUrl: './project-item.component.html',
  styleUrls: ['./project-item.component.less']
})
export class ProjectItemComponent implements OnInit {
  @Input()
  public data: ProjectListDTO;
  @Output()
  public deleteProject = new EventEmitter<string>();
  @Output()
  public editProject = new EventEmitter<string>();
  public constructor() { }
  public ngOnInit() { }
}
