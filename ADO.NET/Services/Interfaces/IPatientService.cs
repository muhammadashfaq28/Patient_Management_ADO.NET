using ADO.NET.Entities;

namespace ADO.NET.Services.Interfaces
{
    public interface IPatientService
    {
        Task<List<Patient>> GetAllPatientsAsync();
    }
}
