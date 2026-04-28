using ADO.NET.Data;
using ADO.NET.Entities;
using ADO.NET.Repositories.Interfaces;
using Microsoft.Data.SqlClient;


namespace ADO.NET.Repositories.Implementations
{
    public class PatientRepository : IPatientRepository
    {
        private readonly DbConnectionFactory _connectionFactory;
        public PatientRepository(DbConnectionFactory connectionFactory) {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<Patient>> GetAllPatientsAsync()
        {
            var patients = new List<Patient>();

            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("GetAllPatients", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var patient = new Patient
                {
                    PatientId = reader["PatientId"] != DBNull.Value ? Convert.ToInt32(reader["PatientId"]) : 0,
                    FirstName = reader["FirstName"]?.ToString(),
                    LastName = reader["LastName"]?.ToString(),
                    Gender = reader["Gender"]?.ToString(),
                    DateOfBirth = reader["DateOfBirth"] != DBNull.Value ? Convert.ToDateTime(reader["DateOfBirth"]) : DateTime.MinValue,
                    City = reader["City"]?.ToString(),
                    PhoneNumber = reader["PhoneNumber"]?.ToString(),
                    Email = reader["Email"]?.ToString()
                };

                patients.Add(patient);
            }

            return patients;
        }

    }
}
