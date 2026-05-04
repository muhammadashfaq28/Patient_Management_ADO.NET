using ADO.NET.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ADO.NET.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService service)
        {
            _appointmentService = service;
        }

        [HttpGet("doctor/{doctorId}")]
        public async Task<IActionResult> GetAppointmentsByDoctor(int doctorId)
        {
            var result = await _appointmentService.GetAppointmentsByDoctorIdAsync(doctorId);

            if (result == null || !result.Any())
                return NotFound();

            return Ok(result);
        }
    }
}