using CarsAndSales.Backend.Controllers;
using CarsAndSales.Repository.Entity;
using CarsAndSales.Services.Interface;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsAndSales.Backend.Tests.Controller
{
    public class SalesControllerTests
    {

        private readonly ISalesServices _salesServices;
        public SalesControllerTests()
        {
            _salesServices = A.Fake<ISalesServices>();
        }

        [Fact]
        public void CarsController_GetSales_ReturnOK()
        {
            //Arrange
            var controller = new SalesController(_salesServices);
            int month = 11;
            int year = 2023;
            //Act
            var result = controller.sales(month, year);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Task<ActionResult>));
        }

        [Fact]
        public void CarsController_PostSales_ReturnOK()
        {
            //Arrange
            var controller = new SalesController(_salesServices);
            var sales = A.Fake<Sales>();
            //Act
            var result = controller.sales(sales);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Task<ActionResult>));
        }
    }
}
