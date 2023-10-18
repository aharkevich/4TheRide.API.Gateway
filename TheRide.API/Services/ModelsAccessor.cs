using TheRide.API.Helpers;
using TheRide.API.Interfaces;
using TheRide.API.Interfaces.Models;

namespace TheRide.API.Services;

/// <inheritdoc />
public class ModelsAccessor : IModelsAccessor
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public ModelsAccessor()
    {
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Model>> GetModelsForCar(string carId, int limit, string? paginationToken)
    {
        ParametersValidators.ValidateNotNullOrWhitespaceParameter(nameof(carId), carId);

        return await Task.FromResult(new[]
        {
            new Model
            {
                Id = Guid.NewGuid().ToString(),
                Name = "X1",
                CarId = "Bmw_Id"
            },
            new Model
            {
                Id = Guid.NewGuid().ToString(),
                Name = "X2",
                CarId = "Bmw_Id"
            }
        });
    }
}