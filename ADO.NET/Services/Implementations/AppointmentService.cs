using ADO.NET.DTOs.Response;
using ADO.NET.Repositories.Interfaces;
using ADO.NET.Services.Interfaces;
using AutoMapper;

namespace ADO.NET.Services.Implementations
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;

        public AppointmentService(IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
        }

        public async Task<List<AppointmentResponseDto>> GetAppointmentsByDoctorIdAsync(int doctorId)
        {
            var appointments = await _appointmentRepository.GetAppointmentsByDoctorIdAsync(doctorId);

            return _mapper.Map<List<AppointmentResponseDto>>(appointments);
        }
    }
}
