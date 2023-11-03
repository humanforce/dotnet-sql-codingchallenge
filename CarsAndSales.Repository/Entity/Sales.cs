using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsAndSales.Repository.Entity
{
    public class Sales
    {
        public Guid Id { get; set; }
        public Guid CarId { get; set; }
        public DateTime Date { get; set; }
    }
}
