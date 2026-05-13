using System.ComponentModel.DataAnnotations;

namespace RoyalVilla_API.Models.DTO
{
    public class VillaDTO
    {
      
        public int Id { get; set; }
        public required string Name { get; set; }
        public string Color { get; set; } = default!;
        public Decimal Price { get; set; }
        public string Details { get; set; } = default!;
        public int Occupancy { get; set; }
        
    }
}
