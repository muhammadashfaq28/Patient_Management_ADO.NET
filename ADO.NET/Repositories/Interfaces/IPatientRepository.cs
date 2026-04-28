using ADO.NET.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;


namespace ADO.NET.Repositories.Interfaces
{
    public interface IPatientRepository
    {
        Task<List<Patient>> GetAllPatientsAsync();
        //Task<Patient> GetPatientByIdAsync(int id);  
        //Task<List<Patient>> GetPatientsWithNoAppointmentasync();
        //Task<List<Patient>> GetPatientsWithAppointmentasync();
        //Task<bool> DeletePatientByIdAsync(int id);

    }
}
