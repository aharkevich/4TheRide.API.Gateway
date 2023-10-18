using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheRide.API.Helpers;
using TheRide.API.Interfaces;
using TheRide.API.Interfaces.Models;
using TheRide.API.Interfaces.Settings;
using TheRide.API.Models;

namespace TheRide.API.Controllers;

/// <summary>
/// API for retrieving cars.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
public class CarsController : ControllerBase
{
    private readonly ICarsAccessor _carsAccessor;
    private readonly CarsStoreSettings _carsStoreSettings;
    private readonly ILogger<CarsController> _logger;

    /// <summary>
    /// Controller.
    /// </summary>
    public CarsController(ICarsAccessor carsAccessor, CarsStoreSettings carsStoreSettings,
        ILogger<CarsController> logger)
    {
        _carsAccessor = carsAccessor ?? throw new ArgumentNullException(nameof(carsAccessor));
        _carsStoreSettings = carsStoreSettings ?? throw new ArgumentNullException(nameof(carsStoreSettings));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Retrieve a list of the cars.
    /// </summary>
    /// <param name="model">Get cars request model.</param>
    /// <returns>The list of the cars.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public async Task<ActionResult<CarResults>> GetCars([FromQuery] GetCarsRequestModel model)
    {
        if (model.Limit > _carsStoreSettings.MaxCarsPerRetrieval)
        {
            _logger.LogInformation("Limit must be less than or equal to {maxRecordsPerRetrieval}.",
                _carsStoreSettings.MaxCarsPerRetrieval);
            return StatusConverters.ConvertToErrorResult(HttpStatusCode.BadRequest, "Limit is invalid.");
        }
        
        var result = await _carsAccessor.GetAllCars(model.Limit, model.PaginationToken);

        return Ok(result);
    }
}