namespace ADO.NET.DTOs.Request
{
    public class UpdatePatientDto
    {


        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? City { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; } = null!;
    }

}
