using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsAndSales.Services.Models
{
    public class CarSalesModel
    {
        public string carName { get; set; }
        public string carColour { get; set; }
        public int quantity { get; set; }
        public int month { get; set; }
        public int year { get; set; }
    }
}
