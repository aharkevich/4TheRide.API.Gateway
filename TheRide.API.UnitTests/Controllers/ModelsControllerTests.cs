using System;
using System.Collections.Generic;
using System.Linq;
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
using TheRide.API.Services;

namespace TheRide.API.UnitTests.Controllers;

[TestClass]
public class ModelsControllerTests
{
    private Mock<ILogger<ModelsController>> _loggerMock;
    private ModelsStoreSettings _modelsStoreSettings = null!;

    [TestInitialize]
    public void BeforeTest()
    {
        _modelsStoreSettings = new ModelsStoreSettings
        {
            MaxModelsPerRetrieval = 1000
        };
        _loggerMock = new Mock<ILogger<ModelsController>>();
    }

    [TestMethod]
    public async Task GetCarModels_WhenLimitIsInvalid_ReturnsBadRequest()
    {
        // Arrange
        var mockModelsAccessor = new Mock<IModelsAccessor>();
        var controller =
            new ModelsController(mockModelsAccessor.Object, _modelsStoreSettings, _loggerMock.Object);

        // Act
        var result = await controller.GetCarModels(new GetCarModelsRequestModel
            { Limit = 1200, CarId = Guid.NewGuid().ToString() });

        // Assert
        result.Result.Should().BeOfType(typeof(ObjectResult));
        (result.Result as ObjectResult)?.Value.Should().BeOfType(typeof(ErrorModel));
        (result.Result as ObjectResult)?.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
    }

    [DataTestMethod]
    [DataRow("")]
    [DataRow(null)]
    public async Task GetCarModels_WhenCarIdIsInvalid_ReturnsBadRequest(string carId)
    {
        // Arrange
        var mockModelsAccessor = new Mock<IModelsAccessor>();
        var controller =
            new ModelsController(mockModelsAccessor.Object, _modelsStoreSettings, _loggerMock.Object);

        // Act
        var result = await controller.GetCarModels(new GetCarModelsRequestModel
            { Limit = 1200, CarId = carId });

        // Assert
        result.Result.Should().BeOfType(typeof(ObjectResult));
        (result.Result as ObjectResult)?.Value.Should().BeOfType(typeof(ErrorModel));
        (result.Result as ObjectResult)?.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
    }

    [TestMethod]
    public async Task GetCarModels_WhenValidParametersAreProvided_ReturnModelsForTheSpecifiedCar()
    {
        // Arrange
        var mockModelsAccessor = new Mock<IModelsAccessor>();
        const string carId = "Bmw_Id";
        var expectedModels = MockedModels.Where(m => m.CarId == carId).ToList();
        mockModelsAccessor.Setup(c => c.GetModelsForCar(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>()))
            .ReturnsAsync(MockedModels);
        mockModelsAccessor.Setup(c => c.GetModelsForCar(carId, It.IsAny<int>(), It.IsAny<string>()))
            .ReturnsAsync(expectedModels);
        var controller = new ModelsController(mockModelsAccessor.Object, _modelsStoreSettings, _loggerMock.Object);

        // Act
        var result = await controller.GetCarModels(new GetCarModelsRequestModel { Limit = 50, CarId = carId });

        // Assert
        result.Result.Should().BeOfType(typeof(OkObjectResult));
        (result.Result as OkObjectResult)?.StatusCode.Should().Be(StatusCodes.Status200OK);
        (result.Result as OkObjectResult)?.Value.Should().BeEquivalentTo(expectedModels);
        mockModelsAccessor.Verify(m => m.GetModelsForCar(carId, It.IsAny<int>(), It.IsAny<string>()), Times.Once);
    }

    private static IEnumerable<Model> MockedModels => new[]
    {
        new Model
        {
            Name = "X1",
            Id = "X1_Id",
            CarId = "Bmw_Id"
            //         Generations = new[]
            //         {
            //             new CarModelGeneration
            //             {
            //                 Name = "E84",
            //                 Year = 2009
            //             },
            //             new CarModelGeneration
            //             {
            //                 Name = "F84",
            //                 Year = 2015
            //             }
        },
        new Model
        {
            Name = "CLA",
            Id = "Cla_Id",
            CarId = "Mercedes_Id"
            //         Generations = new[]
            //         {
            //             new CarModelGeneration
            //             {
            //                 Name = "W168",
            //                 Year = 1997
            //             },
            //             new CarModelGeneration
            //             {
            //                 Name = "W169",
            //                 Year = 2004
            //             }
            //         }
        }
    };
}