using ADO.NET.Entities;
using ADO.NET.Repositories.Interfaces;
using ADO.NET.Services.Interfaces;

namespace ADO.NET.Services.Implementations
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        public PatientService(IPatientRepository patientRepository) {
            _patientRepository = patientRepository;
        }

        public async Task<List<Patient>> GetPatientsAsync()
        {
            return await _patientRepository.GetAllPatientsAsync();
        }
    }
}
