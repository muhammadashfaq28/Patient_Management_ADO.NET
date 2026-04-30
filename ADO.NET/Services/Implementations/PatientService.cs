using ADO.NET.DTOs.Request;
using ADO.NET.DTOs.Response;
using ADO.NET.Entities;
using ADO.NET.Repositories.Interfaces;
using ADO.NET.Services.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

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
        public async Task<bool> CreatePatientAsync(CreatePatientDto dto)
        {
            return await _patientRepository.CreatePatientAsync(dto);
        }


        public async Task<bool> UpdatePatientAsync(int id, UpdatePatientDto dto)
        {
            var patient = _mapper.Map<Patient>(dto);

            patient.PatientId = id;

            return await _patientRepository.UpdatePatientAsync(patient);
        }

        public async Task<bool> DeletePatientByIdAsync(int id)
        {
            return await _patientRepository.DeletePatientByIdAsync(id);
        }

    }
}
