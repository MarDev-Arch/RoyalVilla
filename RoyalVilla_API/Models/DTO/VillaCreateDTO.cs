using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RoyalVilla_API.Models.DTO
{
    public class VillaCreateDTO
    {

        [MaxLength(50)]
        [Required]
        public required string Name { get; set; }
        public string Color { get; set; } = default!;
        [Precision(18, 2)]
        public Decimal Price { get; set; }
        public string Details { get; set; } = default!;
        public int Occupancy { get; set; }
    }
}
