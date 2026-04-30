using ADO.NET.Data;
using ADO.NET.DTOs.Request;
using ADO.NET.DTOs.Response;
using ADO.NET.Entities;
using ADO.NET.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;




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

        public async Task<List<PatientWithAppointmentDto>> GetPatientsWithAppointmentsasync()
        {
            var patientsDict = new Dictionary<int, PatientWithAppointmentDto>();

            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("GetAllPatientsWithAppointment", connection);
            command.CommandType = CommandType.StoredProcedure;

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                int patientId = Convert.ToInt32(reader["PatientId"]);

                if (!patientsDict.ContainsKey(patientId))
                {
                    patientsDict[patientId] = new PatientWithAppointmentDto
                    {
                        Id = patientId,
                        FullName = reader["FirstName"] + " " + reader["LastName"]
                    };
                }

               
                if (reader["AppointmentId"] != DBNull.Value)
                {
                    var appointment = new AppointmentResponseDto
                    {
                        AppointmentId = Convert.ToInt32(reader["AppointmentId"]),
                        AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"])
                 
                    };

                    patientsDict[patientId].Appointments.Add(appointment);
                }
            }

            return patientsDict.Values.ToList();
        }


        public async Task<List<Patient>> GetPatientsWithNoAppointmentasync()
        {
            var patients = new List<Patient>();

            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("GetAllPatienstWithNoAppointment", connection);
            command.CommandType = CommandType.StoredProcedure;

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var patient = new Patient
                {
                    PatientId = Convert.ToInt32(reader["PatientId"]),
                    FirstName = reader["FirstName"]?.ToString(),
                    LastName = reader["LastName"]?.ToString(),
                    City = reader["City"]?.ToString(),
                    Email = reader["Email"]?.ToString()
                };

                patients.Add(patient);
            }

            return patients;
        }


        public async Task<Patient?> GetPatientByIdAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("GetPatientById", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@PatientId", id);

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new Patient
                {
                    PatientId = Convert.ToInt32(reader["PatientId"]),
                    FirstName = reader["FirstName"]?.ToString(),
                    LastName = reader["LastName"]?.ToString(),
                    City = reader["City"]?.ToString(),
                    Email = reader["Email"]?.ToString()
                };
            }

            return null;
        }


        public async Task<bool> CreatePatientAsync(CreatePatientDto dto)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("CreatePatient", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@FirstName", dto.FirstName ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@LastName", dto.LastName ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Gender", dto.Gender ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@DateOfBirth", dto.DateOfBirth);
            command.Parameters.AddWithValue("@City", dto.City ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@PhoneNumber", dto.PhoneNumber ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Email", dto.Email ?? (object)DBNull.Value);

            await connection.OpenAsync();

            var rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0;
        }




        public async Task<bool> UpdatePatientAsync(Patient patient)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("UpdatePatient", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@PatientId", patient.PatientId);
            command.Parameters.AddWithValue("@FirstName", patient.FirstName ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@LastName", patient.LastName ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Gender", patient.Gender ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@DateOfBirth", patient.DateOfBirth);
            command.Parameters.AddWithValue("@City", patient.City ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@PhoneNumber", patient.PhoneNumber ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Email", patient.Email ?? (object)DBNull.Value);

            await connection.OpenAsync();

            var rows = await command.ExecuteNonQueryAsync();

            return rows > 0;
        }



        public async Task<bool> DeletePatientByIdAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("DeletePatientByID", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@PatientId", id);

            await connection.OpenAsync();

            var rows = await command.ExecuteNonQueryAsync();

            return rows > 0;
        }


    }
}
