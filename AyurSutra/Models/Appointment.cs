
using System.ComponentModel.DataAnnotations;

namespace AyurSutra.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }

        public int PatientId { get; set; }

        public int DoctorId { get; set; }

        public int TherapyId { get; set; }

        public DateTime AppointmentDate { get; set; }

        public string Status { get; set; } = "Pending";

        public string Notes { get; set; } = string.Empty;

        public Patient? Patient { get; set; }

        public Doctor? Doctor { get; set; }

        public Therapy? Therapy { get; set; }
    }
}
