import { TestBed } from '@angular/core/testing';

import { DoctorDetailResolver } from './doctor-detail.resolver';

describe('DoctorDetailResolver', () => {
  let resolver: DoctorDetailResolver;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    resolver = TestBed.inject(DoctorDetailResolver);
  });

  it('should be created', () => {
    expect(resolver).toBeTruthy();
  });
});
