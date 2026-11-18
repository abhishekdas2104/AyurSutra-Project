
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AyurSutra.Models
{
    public class Therapy
    {
        [Key]
        public int TherapyId { get; set; }

        [Required]
        public string TherapyName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int Duration { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
    }
}
