import { TestBed } from '@angular/core/testing';

import { ConfigServerService } from './config-server.service';

describe('ConfigServerService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ConfigServerService = TestBed.get(ConfigServerService);
    expect(service).toBeTruthy();
  });
});
