using TheRide.API.Interfaces.Models;

namespace TheRide.API.Interfaces;

/// <summary>
/// Provide access to car models.
/// </summary>
public interface IModelsAccessor
{
    /// <summary>
    /// Get all the car models for the specified car.
    /// </summary>
    /// <param name="carId">Car identifier.</param>
    /// <param name="limit">Upper limit on the number of items to return per request.</param>
    /// <param name="paginationToken">A token that can be used to retrieve the next "page" of car models.</param>
    /// <returns>List of car models.</returns>
    /// <exception cref="ArgumentException">
    /// Limit must be in valid range.
    /// Car Id should be provided.
    /// </exception>
    public Task<IEnumerable<Model>> GetModelsForCar(string carId, int limit, string? paginationToken);
}