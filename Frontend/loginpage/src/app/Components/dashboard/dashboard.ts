import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { PatientService } from '../../services/patientService';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Patient } from '../../models/PatientInterface';


@Component({
  selector: 'app-dashboard',
  imports: [ReactiveFormsModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})
export class Dashboard {

  addForm: FormGroup;
  updateForm: FormGroup;
  selectedId: number = 0;
  view: string = '';
  patients: Patient[] = [];

  constructor(
    private fb: FormBuilder,
    private patientService: PatientService,
    private router: Router
  ) {
    this.addForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      gender: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      city: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]]
    });

    this.updateForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      gender: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      city: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]]
    });
  }



  formatDate(date: any) {
    return date ? date.split('T')[0] : '';
  }
  editPatient(patient: any) {

    this.view = 'update';     // show form
    this.selectedId = patient.patientId;

    this.updateForm.patchValue({
      firstName: patient.firstName ?? '',
      lastName: patient.lastName ?? '',
      gender: patient.gender ?? '',
      dateOfBirth: this.formatDate(patient.dateOfBirth),
      city: patient.city,
      phoneNumber: patient.phoneNumber ?? '',
      email: patient.email
    });
  }

  addPatient() {
    if (this.addForm.invalid) return;

    const formValue = this.addForm.value;

    const body = {
      ...formValue,
      dateOfBirth: new Date(formValue.dateOfBirth).toISOString()
    };

    this.patientService.createPatient(body).subscribe({
      next: (res) => {
        alert('Patient added successfully');
        this.addForm.reset();
      },
      error: (err) => {
        console.error(err);
        alert('Failed to add patient');
      }
    });
  }

  updatePatient() {

    const body = {
      ...this.updateForm.value,
      dateOfBirth: new Date(this.updateForm.value.dateOfBirth).toISOString()
    };

    this.patientService.updatePatient(this.selectedId, body).subscribe({
      next: () => {
        alert('Updated successfully');
        this.updateForm.reset();
        this.view = '';
      },
      error: (err) => {
        console.error(err);
      }
    });
  }

  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }

  loadPatients() {
    this.patientService.getAll().subscribe(res => {
      this.patients = res;
      this.view = 'patients';
    });
  }

  showAdd() { this.view = 'add'; }
  showUpdate() { this.view = 'update'; }
  showDelete() { this.view = 'delete'; }
}