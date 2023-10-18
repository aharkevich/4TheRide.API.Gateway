using TheRide.API.Interfaces.Models;

namespace TheRide.API.Interfaces;

/// <summary>
/// Provide access to cars.
/// </summary>
public interface ICarsAccessor
{
    /// <summary>
    /// Get all the cars.
    /// </summary>
    /// <param name="limit">Upper limit on the number of items to return per request.</param>
    /// <param name="paginationToken">A token that can be used to retrieve the next "page" of cars.</param>
    /// <returns>List of cars.</returns>
    /// <exception cref="ArgumentException">
    /// Limit must be in valid range.
    /// </exception>
    public Task<IEnumerable<Car>> GetAllCars(int limit, string? paginationToken);
}