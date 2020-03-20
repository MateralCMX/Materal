import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfigurationItemEditComponent } from './configuration-item-edit.component';

describe('ConfigurationItemEditComponent', () => {
  let component: ConfigurationItemEditComponent;
  let fixture: ComponentFixture<ConfigurationItemEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConfigurationItemEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConfigurationItemEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
