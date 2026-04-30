using ADO.NET.Entities;

namespace ADO.NET.Repositories.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<List<Appointment>> GetAppointmentsByDoctorIdAsync(int doctorId);
    }
}
