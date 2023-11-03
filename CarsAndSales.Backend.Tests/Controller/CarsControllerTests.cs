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
    public class CarsControllerTests
    {

        private readonly ICarServices _carServices;
        public CarsControllerTests()
        {
            _carServices = A.Fake<ICarServices>();
        }

        [Fact]
        public void CarsController_GetCars_ReturnOK()
        {
            //Arrange
            var controller = new CarsController(_carServices);

            //Act
            var result = controller.cars();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Task<ActionResult>));
        }

        [Fact]
        public void CarsController_PostCars_ReturnOK()
        {
            //Arrange
            var car = A.Fake<Car>();
            var controller = new CarsController(_carServices);

            //Act
            var result = controller.cars(car);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Task<ActionResult>));
        }
    }
}
