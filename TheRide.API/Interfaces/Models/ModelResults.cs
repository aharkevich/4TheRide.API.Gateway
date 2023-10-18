namespace TheRide.API.Interfaces.Models;

/// <summary>
/// Results of getting car models.
/// </summary>
public class ModelResults
{
    /// <summary>
    /// A token that can be used to retrieve the next "page" of car models.
    /// </summary>
    public string PaginationToken { get; set; } = string.Empty;

    /// <summary>
    /// The current page of car models.
    /// </summary>
    public IEnumerable<Model> Page { get; set; } = Array.Empty<Model>();
}