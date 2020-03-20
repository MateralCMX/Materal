import { TestBed } from '@angular/core/testing';

import { ConfigurationItemService } from './configuration-item.service';

describe('ConfigurationItemService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ConfigurationItemService = TestBed.get(ConfigurationItemService);
    expect(service).toBeTruthy();
  });
});
