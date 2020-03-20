import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfigurationItemListComponent } from './configuration-item-list.component';

describe('ConfigurationItemListComponent', () => {
  let component: ConfigurationItemListComponent;
  let fixture: ComponentFixture<ConfigurationItemListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConfigurationItemListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConfigurationItemListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
