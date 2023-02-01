import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DefaultDataSettingComponent } from './default-data-setting.component';

describe('DefaultDataSettingComponent', () => {
  let component: DefaultDataSettingComponent;
  let fixture: ComponentFixture<DefaultDataSettingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DefaultDataSettingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DefaultDataSettingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
