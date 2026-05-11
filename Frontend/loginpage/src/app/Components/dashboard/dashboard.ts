import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { PatientService } from '../../services/patientService';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { error } from 'console';

@Component({
  selector: 'app-dashboard',
  imports: [ReactiveFormsModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})
export class Dashboard {

  addForm: FormGroup;
  view: string = '';
  patients: any[] = [];

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

  addPatient() {

    if (this.addForm.invalid) return;

    const formValue = this.addForm.value;

    const body = {
      ...formValue,
      dateOfBirth: new Date(formValue.dateOfBirth).toISOString()
    };

    console.log(body);

    this.patientService.add(body).subscribe({
      next: (res) => {
        console.log(res);
        alert('Patient added successfully');
        this.addForm.reset();
      },
      error: (err) => {
        console.error(err);
        alert('Failed to add patient');
      }
    });
  }

  showAdd() { this.view = 'add'; }
  showUpdate() { this.view = 'update'; }
  showDelete() { this.view = 'delete'; }
}