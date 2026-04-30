using ADO.NET.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using ADO.NET.DTOs.Response;


namespace ADO.NET.Repositories.Interfaces
{
    public interface IPatientRepository
    {
        Task<List<Patient>> GetAllPatientsAsync();
        Task<Patient> GetPatientByIdAsync(int id);
        Task<List<Patient>> GetPatientsWithNoAppointmentasync();
        Task<List<PatientWithAppointmentDto>> GetPatientsWithAppointmentsasync();
        //Task<bool> DeletePatientByIdAsync(int id);

    }
}
