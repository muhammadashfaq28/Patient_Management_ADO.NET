using ADO.NET.DTOs.Request;
using ADO.NET.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;


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



        [HttpPost]
        public async Task<IActionResult> CreatePatient(CreatePatientDto dto)
        {
            var result = await _patientService.CreatePatientAsync(dto);

            if (!result)
                return BadRequest("Failed to create patient");

            return Ok("Patient created successfully");
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, UpdatePatientDto dto)
        {
            var result = await _patientService.UpdatePatientAsync(id, dto);

            if (!result)
                return NotFound($"Patient with ID {id} not found");

            return Ok(new { message = "Patient created successfully" });
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var result = await _patientService.DeletePatientByIdAsync(id);

            if (!result)
                return NotFound($"Patient with ID {id} not found");

            return Ok("Patient deleted successfully");
        }


    }
}
