using CarsAndSales.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsAndSales.Services.Interface
{
    public interface IUserServices
    {
        string Login(string userName, string password);
    }
}
