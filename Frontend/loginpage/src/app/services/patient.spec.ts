import { TestBed } from '@angular/core/testing';

import { PatientService } from './patientService';

describe('PatientService', () => {

  let service: PatientService;

  beforeEach(() => {

    TestBed.configureTestingModule({});

    service = TestBed.inject(PatientService);

  });

  it('should be created', () => {

    expect(service).toBeTruthy();

  });

});