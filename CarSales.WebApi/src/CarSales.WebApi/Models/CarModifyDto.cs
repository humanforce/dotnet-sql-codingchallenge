using CarSales.Common.Database;
using System.ComponentModel.DataAnnotations;

namespace CarSales.WebApi.Models
{
    public class CarModifyDto
    {
        [StringLength(128)]
        public virtual string? Name { get; set; }

        [StringLength(128)]
        public virtual string? Colour { get; set; }

        public virtual decimal? Price { get; set; }

        public Car ToDbModel()
        {
            return new Car
            {
                Name = this.Name,
                Colour = this.Colour,
                Price = this.Price ?? 0
            };
        }

        public Car ToDbModel(Car car)
        {
            if (Name != null)
            {
                car.Name = Name;
            }

            if (Colour != null)
            {
                car.Colour = Colour;
            }

            if (Price != null)
            {
                car.Price = Price ?? 0;
            }

            return car;
        }
    }
}
