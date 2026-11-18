
using System.ComponentModel.DataAnnotations;

namespace AyurSutra.Models
{
    public class TreatmentRecord
    {
        [Key]
        public int RecordId { get; set; }

        public int PatientId { get; set; }

        public int TherapyId { get; set; }

        public DateTime TreatmentDate { get; set; }

        public string Observations { get; set; } = string.Empty;

        public string Status { get; set; } = "Completed";

        public Patient? Patient { get; set; }

        public Therapy? Therapy { get; set; }

    }
}
