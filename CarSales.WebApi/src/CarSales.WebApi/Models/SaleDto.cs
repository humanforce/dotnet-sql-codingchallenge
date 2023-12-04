using System.ComponentModel.DataAnnotations;

namespace CarSales.WebApi.Models
{
    public class SaleDto
    {
        [Required]
        public Guid? CarId { get; set; }

        [Required]
        public DateOnly? Date { get; set; }
    }
}
