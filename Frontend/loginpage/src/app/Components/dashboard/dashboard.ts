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

  // ─── Properties ───────────────────────
  view: string = '';
  patients: Patient[] = [];
  selectedId: number = 0;

  addForm: FormGroup;
  updateForm: FormGroup;

  // ─── Constructor ──────────────────────
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

  // ─── Helper ───────────────────────────
  private formatDate(date: any): string {
    return date ? date.split('T')[0] : '';
  }

  // ─── Load ─────────────────────────────
 loadPatients() {
  this.view = 'patients'; 
  this.patientService.getAll().subscribe({
    next: (res) => {
      this.patients = res;
    },
    error: (err) => {
      console.error(err);
    }
  });
}

  // ─── Add ──────────────────────────────
  addPatient() {
    if (this.addForm.invalid) return;

    const body = {
      ...this.addForm.value,
      dateOfBirth: new Date(this.addForm.value.dateOfBirth).toISOString()
    };

    this.patientService.createPatient(body).subscribe({
      next: () => {
        alert('Patient added successfully');
        this.addForm.reset();
        this.loadPatients();
      },
      error: () => alert('Failed to add patient')
    });
  }

  // ─── Edit ─────────────────────────────
  editPatient(patient: any) {
    this.view = 'update';
    this.selectedId = patient.patientId;

    this.updateForm.patchValue({
      firstName: patient.firstName ?? '',
      lastName: patient.lastName ?? '',
      gender: patient.gender ?? '',
      dateOfBirth: this.formatDate(patient.dateOfBirth),
      city: patient.city ?? '',
      phoneNumber: patient.phoneNumber ?? '',
      email: patient.email ?? ''
    });
  }

  // ─── Update ───────────────────────────
  updatePatient() {
    if (this.updateForm.invalid) return;

    const body = {
      ...this.updateForm.value,
      dateOfBirth: new Date(this.updateForm.value.dateOfBirth).toISOString()
    };

    this.patientService.updatePatient(this.selectedId, body).subscribe({
      next: () => {
        alert('Updated successfully');
        this.updateForm.reset();
        this.loadPatients(); 
      },
      error: () => alert('Failed to update patient')
    });
  }

  // ─── Delete ───────────────────────────
  deletePatient(id: number) {
    if (!confirm('Are you sure?')) return;

    this.patientService.deletePatient(id).subscribe({
      next: () => {
        alert('Patient deleted successfully');
        this.patients = this.patients.filter(p => p.patientId !== id); // ✅ locally remove
      },
      error: () => alert('Failed to delete patient')
    });
  }

  // ─── Auth ─────────────────────────────
  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }

  // ─── Navigation ───────────────────────
  showAdd() { this.view = 'add'; }
}