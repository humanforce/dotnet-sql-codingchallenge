using System.ComponentModel.DataAnnotations;

namespace CarSales.WebApi.Models
{
    public class CarCreateDto : CarModifyDto
    {
        [Required]
        public override string? Name { get; set; }

        [Required]
        public override string? Colour { get; set; }

        [Required]
        public override decimal? Price { get; set; }
    }
}
