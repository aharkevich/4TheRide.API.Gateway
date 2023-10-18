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
/// API for retrieving car models.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
public class ModelsController : ControllerBase
{
    private readonly IModelsAccessor _modelsAccessor;
    private readonly ModelsStoreSettings _modelsStoreSettings;
    private readonly ILogger<ModelsController> _logger;

    public ModelsController(IModelsAccessor modelsAccessor, ModelsStoreSettings modelsStoreSettings,
        ILogger<ModelsController> logger)
    {
        _modelsAccessor = modelsAccessor;
        _modelsStoreSettings = modelsStoreSettings;
        _logger = logger;
    }

    /// <summary>
    /// Retrieve a list of the car models.
    /// </summary>
    /// <param name="model">Get car models request model.</param>
    /// <returns>The list of the car models.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public async Task<ActionResult<ModelResults>> GetCarModels([FromQuery] GetCarModelsRequestModel model)
    {
        if (model.Limit > _modelsStoreSettings.MaxModelsPerRetrieval)
        {
            _logger.LogInformation("Limit must be less than or equal to {MaxRecordsPerRetrieval}.",
                _modelsStoreSettings.MaxModelsPerRetrieval);
            return StatusConverters.ConvertToErrorResult(HttpStatusCode.BadRequest, "Limit is invalid.");
        }

        try
        {
            var result = await _modelsAccessor.GetModelsForCar(model.CarId, model.Limit, model.PaginationToken);
            return Ok(result);
        }
        catch (ArgumentException ex) when (ParametersValidators.IsKnownArgumentException(ex.ParamName))
        {
            _logger.LogInformation("Failed to get models for the car. Exception: {Message}.", ex.Message);
            var errorMessage = $"Failed to get car models as {ex.ParamName} is invalid.";
            return StatusConverters.ConvertToErrorResult(HttpStatusCode.BadRequest, errorMessage);
        }
    }
}