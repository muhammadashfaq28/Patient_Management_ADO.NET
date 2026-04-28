using ADO.NET.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;

namespace ADO.NET.Services.Interfaces
{
    public interface IPatientService
    {
        Task<List<Patient>> GetAllPatientsAsync();
    }
}
