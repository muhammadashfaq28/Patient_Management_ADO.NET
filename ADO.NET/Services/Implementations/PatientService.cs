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
    }
}
