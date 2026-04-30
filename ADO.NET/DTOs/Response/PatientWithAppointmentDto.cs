namespace ADO.NET.DTOs.Response
{
    public class PatientWithAppointmentDto
    {
        public int Id { get; set; } 
        public string? FullName { get; set; }
        public List<AppointmentResponseDto> Appointments { get; set; } = new();

    }
}
