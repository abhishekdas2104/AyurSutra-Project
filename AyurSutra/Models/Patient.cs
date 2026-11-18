
using System.ComponentModel.DataAnnotations;

namespace AyurSutra.Models
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Gender { get; set; }

        public int Age { get; set; }

        [Required]
        public string Phone { get; set; }

        public string Address { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;
    }
}
