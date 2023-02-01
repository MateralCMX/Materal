import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WebAppListComponent } from './web-app-list.component';

describe('WebAPPListComponent', () => {
  let component: WebAppListComponent;
  let fixture: ComponentFixture<WebAppListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WebAppListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WebAppListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
