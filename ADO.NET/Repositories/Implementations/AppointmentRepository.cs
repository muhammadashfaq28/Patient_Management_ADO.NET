using ADO.NET.Data;
using ADO.NET.Entities;
using ADO.NET.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly DbConnectionFactory _connectionFactory;

    public AppointmentRepository(DbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<List<Appointment>> GetAppointmentsByDoctorIdAsync(int doctorId)
    {
        var appointments = new List<Appointment>();

        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand("GetAppointmentsByDoctor", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@DoctorId", doctorId);

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var appointment = new Appointment
            {
                AppointmentId = Convert.ToInt32(reader["AppointmentId"]),
                AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]),
                PatientId = Convert.ToInt32(reader["PatientId"]),
                DoctorId = doctorId
            };

            appointments.Add(appointment);
        }

        return appointments;
    }
}