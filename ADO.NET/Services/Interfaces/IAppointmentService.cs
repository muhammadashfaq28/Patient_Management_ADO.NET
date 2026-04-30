using ADO.NET.DTOs.Response;

namespace ADO.NET.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<List<AppointmentResponseDto>> GetAppointmentsByDoctorIdAsync(int doctorId);
    }
}
