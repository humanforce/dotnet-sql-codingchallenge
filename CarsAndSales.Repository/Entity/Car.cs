using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsAndSales.Repository.Entity
{
    public class Car
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Colour { get; set; }
        public int Price { get; set; }
    }
}
