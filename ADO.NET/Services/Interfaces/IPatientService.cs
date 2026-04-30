using ADO.NET.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using ADO.NET.DTOs.Response;

namespace ADO.NET.Services.Interfaces
{
    public interface IPatientService
    {
        Task<List<PatientResponseDto>> GetAllPatientsAsync();
        Task<List<PatientWithAppointmentDto>> GetPatientWithAppointmentsAsync();

        Task<List<PatientResponseDto>> GetPatientsWithNoAppointmentsAsync();

        Task<PatientResponseDto?> GetPatientByIdAsync(int id);
      
    }
}
