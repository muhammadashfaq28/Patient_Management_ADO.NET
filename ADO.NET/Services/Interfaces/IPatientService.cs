using ADO.NET.DTOs.Request;
using ADO.NET.DTOs.Response;
using ADO.NET.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ADO.NET.Services.Interfaces
{
    public interface IPatientService
    {
        Task<List<PatientResponseDto>> GetAllPatientsAsync();
        Task<List<PatientWithAppointmentDto>> GetPatientWithAppointmentsAsync();

        Task<List<PatientResponseDto>> GetPatientsWithNoAppointmentsAsync();

        Task<PatientResponseDto?> GetPatientByIdAsync(int id);
        Task<bool> CreatePatientAsync(CreatePatientDto dto);

        Task<bool> UpdatePatientAsync(int id, UpdatePatientDto dto);

        Task<bool> DeletePatientByIdAsync(int id);
    }
}
