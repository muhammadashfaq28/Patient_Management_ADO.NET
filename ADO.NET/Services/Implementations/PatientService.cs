using ADO.NET.Entities;
using ADO.NET.Repositories.Interfaces;
using ADO.NET.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;

namespace ADO.NET.Services.Implementations
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        public PatientService(IPatientRepository patientRepository) {
            _patientRepository = patientRepository;
        }

        public async Task<List<Patient>> GetAllPatientsAsync()
        {
            return await _patientRepository.GetAllPatientsAsync();
        }
    }
}
