import { TestBed } from '@angular/core/testing';

import { InkService } from './ink.service';

describe('InkService', () => {
  let service: InkService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(InkService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
