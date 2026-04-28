using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
namespace ADO.NET.Entities
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
    }
}
