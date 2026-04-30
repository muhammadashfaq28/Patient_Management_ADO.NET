using ADO.NET.Entities;
using ADO.NET.Repositories.Interfaces;
using ADO.NET.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using AutoMapper;
using ADO.NET.DTOs.Response;

namespace ADO.NET.Services.Implementations
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;
        public PatientService(IPatientRepository patientRepository, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public async Task<List<PatientResponseDto>> GetAllPatientsAsync()
        {
            var patients = await _patientRepository.GetAllPatientsAsync();

            return _mapper.Map<List<PatientResponseDto>>(patients);
        }

        public async Task<List<PatientWithAppointmentDto>> GetPatientWithAppointmentsAsync()
        {
            return await _patientRepository.GetPatientsWithAppointmentsasync();
        }


        public async Task<List<PatientResponseDto>> GetPatientsWithNoAppointmentsAsync()
        {
            var patients = await _patientRepository.GetPatientsWithNoAppointmentasync();

            return _mapper.Map<List<PatientResponseDto>>(patients);
        }


        public async Task<PatientResponseDto?> GetPatientByIdAsync(int id)
        {
            var patient = await _patientRepository.GetPatientByIdAsync(id);

            if (patient == null)
                return null;

            return _mapper.Map<PatientResponseDto>(patient);
        }
    }
}
