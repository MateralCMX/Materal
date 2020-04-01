import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-test',
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.less']
})
export class TestComponent implements OnInit {

  constructor(private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    const url = window.localStorage.getItem('subAppUrl');
    console.log('localStorage subAppUrl = ' + url);
    console.log(`路由参数id=${this.route.snapshot.params.id}`);
    (window as any).testFunction();
  }
}
