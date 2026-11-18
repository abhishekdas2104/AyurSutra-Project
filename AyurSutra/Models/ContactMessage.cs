
using System.ComponentModel.DataAnnotations;

namespace AyurSutra.Models
{
    public class ContactMessage
    {
        [Key]
        public int MessageId { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Subject { get; set; } = string.Empty;

        [Required]
        public string Message { get; set; } = string.Empty;

        public DateTime SubmittedAt { get; set; } = DateTime.Now;
    }
}
