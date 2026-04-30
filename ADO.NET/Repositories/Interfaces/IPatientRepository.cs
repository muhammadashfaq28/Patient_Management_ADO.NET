using ADO.NET.DTOs.Request;
using ADO.NET.DTOs.Response;
using ADO.NET.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;


namespace ADO.NET.Repositories.Interfaces
{
    public interface IPatientRepository
    {
        Task<List<Patient>> GetAllPatientsAsync();
        Task<Patient> GetPatientByIdAsync(int id);
        Task<List<Patient>> GetPatientsWithNoAppointmentasync();
        Task<List<PatientWithAppointmentDto>> GetPatientsWithAppointmentsasync();
        Task<bool> DeletePatientByIdAsync(int id);

        Task<bool> CreatePatientAsync(CreatePatientDto dto);

        Task<bool> UpdatePatientAsync(Patient patient);

    }
}
