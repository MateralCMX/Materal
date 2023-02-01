import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WebAppEditComponent } from './web-app-edit.component';

describe('WebAppEditComponent', () => {
  let component: WebAppEditComponent;
  let fixture: ComponentFixture<WebAppEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WebAppEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WebAppEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
