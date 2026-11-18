
using System.ComponentModel.DataAnnotations;

namespace AyurSutra.Models
{
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string Specialization { get; set; } = string.Empty;

        [Required]
        public string Phone { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Qualification { get; set; } = string.Empty;
    }
}
