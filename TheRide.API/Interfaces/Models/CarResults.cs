namespace TheRide.API.Interfaces.Models;

/// <summary>
/// Results of getting cars.
/// </summary>
public class CarResults
{
    /// <summary>
    /// A token that can be used to retrieve the next "page" of cars.
    /// </summary>
    public string PaginationToken { get; set; } = string.Empty;

    /// <summary>
    /// The current page of cars.
    /// </summary>
    public IEnumerable<Car> Page { get; set; } = Array.Empty<Car>();
}