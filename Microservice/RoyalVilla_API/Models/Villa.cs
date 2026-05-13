using System.ComponentModel.DataAnnotations;

namespace RoyalVilla_API.Models
{
    public class Villa
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }
        public string Color { get; set; } =default!;
        public Decimal Price { get; set; }
        public string Details { get; set; } =default!;
        public int Occupancy { get; set; } 
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}
