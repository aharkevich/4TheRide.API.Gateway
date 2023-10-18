using TheRide.API.Interfaces;
using TheRide.API.Interfaces.Models;

namespace TheRide.API.Services;

/// <inheritdoc />
public class CarsAccessor : ICarsAccessor
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public CarsAccessor()
    {
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Car>> GetAllCars(int limit, string? paginationToken)
    {
        return await Task.FromResult(new[]
        {
            new Car
            {
                Name = "BMW",
                Id = Guid.NewGuid().ToString()
            }
        });
    }
}