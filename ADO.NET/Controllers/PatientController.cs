using ADO.NET.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;


namespace ADO.NET.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        public PatientController(IPatientService patientService) { 
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPatients()
        {
            var patients = await _patientService.GetAllPatientsAsync();
            return Ok(patients);
        }

        [HttpGet("with-appointments")]
        public async Task<IActionResult> GetPatientsWithAppointments()
        {
            var result = await _patientService.GetPatientWithAppointmentsAsync();

            return Ok(result);
        }


        [HttpGet("no-appointments")]
        public async Task<IActionResult> GetPatientsWithNoAppointments()
        {
            var result = await _patientService.GetPatientsWithNoAppointmentsAsync();

            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientById(int id)
        {
            var result = await _patientService.GetPatientByIdAsync(id);

            if (result == null)
                return NotFound($"Patient with ID {id} not found");

            return Ok(result);
        }



    }
}
