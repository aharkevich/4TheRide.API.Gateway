using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TheRide.API.Controllers;
using TheRide.API.Interfaces;
using TheRide.API.Interfaces.Models;
using TheRide.API.Interfaces.Settings;
using TheRide.API.Models;

namespace TheRide.API.UnitTests.Controllers;

[TestClass]
public class CarsControllerTests
{
    private Mock<ILogger<CarsController>> _loggerMock;
    private CarsStoreSettings _carsStoreSettings = null!;

    [TestInitialize]
    public void BeforeTest()
    {
        _carsStoreSettings = new CarsStoreSettings
        {
            MaxCarsPerRetrieval = 1000
        };
        _loggerMock = new Mock<ILogger<CarsController>>();
    }

    [TestMethod]
    public async Task GetCars_WhenLimitIsInvalid_ReturnsBadRequest()
    {
        // Arrange
        var mockCarsAccessor = new Mock<ICarsAccessor>();
        var controller = new CarsController(mockCarsAccessor.Object, _carsStoreSettings, _loggerMock.Object);

        // Act
        var result = await controller.GetCars(new GetCarsRequestModel { Limit = 1200 });

        // Assert
        result.Result.Should().BeOfType(typeof(ObjectResult));
        (result.Result as ObjectResult)?.Value.Should().BeOfType(typeof(ErrorModel));
        (result.Result as ObjectResult)?.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
    }

    [TestMethod]
    public async Task GetCars_WhenValidParametersAreProvided_ReturnCars()
    {
        // Arrange
        var mockCarsAccessor = new Mock<ICarsAccessor>();
        mockCarsAccessor.Setup(c => c.GetAllCars(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(MockedCars);
        var controller = new CarsController(mockCarsAccessor.Object, _carsStoreSettings, _loggerMock.Object);

        // Act
        var result = await controller.GetCars(new GetCarsRequestModel { Limit = 50 });

        // Assert
        result.Result.Should().BeOfType(typeof(OkObjectResult));
        (result.Result as OkObjectResult)?.StatusCode.Should().Be(StatusCodes.Status200OK);
        (result.Result as OkObjectResult)?.Value.Should().BeEquivalentTo(MockedCars);
    }

    private static IEnumerable<Car> MockedCars => new[]
    {
        new Car
        {
            Name = "BMW",
            Id = "Bmw_Id"
        },
        new Car
        {
            Name = "Mercedes-Benz",
            Id = "Mercedes_Id"
        }
    };
}